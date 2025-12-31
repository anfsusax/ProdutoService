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
        var itens = new List<PedidoItem>();

        foreach (var item in command.Itens)
        {
            var produto = await _produtoCatalogClient.ObterPorIdAsync(item.ProdutoId, cancellationToken);

            if (produto is null)
            {
                throw new InvalidOperationException($"Produto {item.ProdutoId} não encontrado no catálogo.");
            }

            if (!produto.Ativo)
            {
                throw new InvalidOperationException($"Produto {item.ProdutoId} está inativo e não pode ser adicionado ao pedido.");
            }

            var pedidoItem = new PedidoItem(produto.Id, produto.Nome, item.Quantidade, produto.Preco);
            itens.Add(pedidoItem);
        }

        var pedido = new PedidoEntity(itens);
        
        // Associar os itens ao pedido
        foreach (var item in pedido.Itens)
        {
            item.AssociarPedido(pedido.Id);
        }
        
        await _pedidoRepository.AdicionarAsync(pedido);

        return pedido.Id;
    }
}
