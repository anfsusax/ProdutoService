using Pedido.Domain.Exceptions;

namespace Pedido.Domain.Entities;

public class PedidoModel
{
    public Guid Id { get; private set; }
    public decimal ValorTotal { get; private set; }
    public DateTime DataPedido { get; private set; }
    public string Status { get; private set; }
    public List<PedidoItem> Itens { get; private set; } = new();

    protected PedidoModel() { }

    public PedidoModel(IEnumerable<PedidoItem> itens)
    {
        if (itens is null || !itens.Any())
            throw new DomainException("Pedido deve conter pelo menos um item.");

        Id = Guid.NewGuid();
        DataPedido = DateTime.UtcNow;
        Status = "Criado";

        Itens = itens.ToList();
        ValorTotal = Itens.Sum(i => i.Quantidade * i.PrecoUnitario);
    }
}
