using Pedido.Application.Interfaces;
using Pedido.Domain.Entities;
using PedidoEntity = global::Pedido.Domain.Entities.PedidoModel;

namespace Pedido.Application.Commands.Pedidos.CriarPedido;

public class CriarPedidoHandler
{
    private readonly IPedidoRepository _pedidoRepository;
    private readonly IProdutoCatalogClient _produtoCatalogClient;

    public CriarPedidoHandler(IPedidoRepository pedidoRepository, IProdutoCatalogClient produtoCatalogClient)
    {
        _pedidoRepository = pedidoRepository;
        _produtoCatalogClient = produtoCatalogClient;
    }

    public async Task<Guid> Handle(CriarPedidoCommand command, CancellationToken cancellationToken = default)
    { 
        var itensAgrupados = command.Itens
            .GroupBy(i => i.ProdutoId)
            .Select(g => new CriarPedidoItemDto(g.Key, g.Sum(i => i.Quantidade)))
            .ToList();
         
        var validacoes = await Task.WhenAll(
            itensAgrupados.Select(async item =>
            {
                var produto = await _produtoCatalogClient.ObterPorIdAsync(item.ProdutoId, cancellationToken);
                return new { Item = item, Produto = produto };
            })
        );
         
        var erros = new List<string>();
        var itensValidos = new List<PedidoItem>();

        foreach (var validacao in validacoes)
        {
            if (validacao.Produto is null)
            {
                erros.Add($"Produto {validacao.Item.ProdutoId} não encontrado no catálogo.");
                continue;
            }

            if (!validacao.Produto.Ativo)
            {
                erros.Add($"Produto {validacao.Item.ProdutoId} ({validacao.Produto.Nome}) está inativo e não pode ser adicionado ao pedido.");
                continue;
            }

            var pedidoItem = new PedidoItem(
                validacao.Produto.Id,
                validacao.Produto.Nome,
                validacao.Item.Quantidade,
                validacao.Produto.Preco);
            
            itensValidos.Add(pedidoItem);
        }
         
        if (erros.Any())
        {
            throw new InvalidOperationException(
                $"Erros ao validar produtos do pedido:\n{string.Join("\n", erros)}");
        }
         
        var pedido = new PedidoEntity(itensValidos);
         
        foreach (var item in pedido.Itens)
        {
            item.AssociarPedido(pedido.Id);
        }
        
        await _pedidoRepository.AdicionarAsync(pedido);

        return pedido.Id;
    }
}
