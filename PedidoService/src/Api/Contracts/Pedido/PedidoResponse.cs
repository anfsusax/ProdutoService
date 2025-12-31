namespace Pedido.Api.Contracts.Pedido;

public class PedidoResponse
{
    public Guid Id { get; set; }
    public decimal ValorTotal { get; set; }
    public DateTime DataPedido { get; set; }
    public string Status { get; set; } = string.Empty;
    public List<PedidoItemResponse> Itens { get; set; } = new();
}

public class PedidoItemResponse
{
    public Guid Id { get; set; }
    public Guid ProdutoId { get; set; }
    public string Nome { get; set; } = string.Empty;
    public int Quantidade { get; set; }
    public decimal PrecoUnitario { get; set; }
}

