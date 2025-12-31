using Pedido.Domain.Exceptions;

namespace Pedido.Domain.Entities;

public class PedidoItem
{
    public Guid Id { get; private set; }
    public Guid PedidoId { get; private set; }
    public Guid ProdutoId { get; private set; }
    public string Nome { get; private set; } = default!;
    public int Quantidade { get; private set; }
    public decimal PrecoUnitario { get; private set; }

    // EF Core
    protected PedidoItem() { }

    public PedidoItem(Guid produtoId, string nome, int quantidade, decimal precoUnitario)
    {
        if (string.IsNullOrWhiteSpace(nome)) throw new DomainException("Nome do item é obrigatório.");
        if (quantidade <= 0) throw new DomainException("Quantidade deve ser maior que zero.");
        if (precoUnitario <= 0) throw new DomainException("Preço unitário deve ser maior que zero.");

        Id = Guid.NewGuid();
        ProdutoId = produtoId;
        Nome = nome;
        Quantidade = quantidade;
        PrecoUnitario = precoUnitario;
    }

    public void AssociarPedido(Guid pedidoId)
    {
        PedidoId = pedidoId;
    }
}
