using Pedido.Domain.Exceptions;

namespace Pedido.Domain.Entities;

public class PedidoModel
{
    public Guid Id { get; private set; }
    public decimal ValorTotal { get; private set; }
    public DateTime DataPedido { get; private set; }
    public string Status { get; private set; }
    // public List<PedidoItem> Itens { get; private set; } = new(); // Implementing simple version first

    protected PedidoModel() { }

    public PedidoModel(decimal valorTotal)
    {
        if (valorTotal <= 0) throw new DomainException("Valor total deve ser maior que zero.");
        
        Id = Guid.NewGuid();
        ValorTotal = valorTotal;
        DataPedido = DateTime.UtcNow;
        Status = "Criado";
    }
}
