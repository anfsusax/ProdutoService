using System.ComponentModel.DataAnnotations;

namespace Pedido.Api.Contracts.Pedido;

public class CriarPedidoRequest
{
    [Required(ErrorMessage = "A lista de itens é obrigatória.")]
    [MinLength(1, ErrorMessage = "O pedido deve conter pelo menos um item.")]
    public List<CriarPedidoItemRequest> Itens { get; set; } = new();
}

public class CriarPedidoItemRequest
{
    [Required(ErrorMessage = "O ID do produto é obrigatório.")]
    public Guid ProdutoId { get; set; }

    [Required(ErrorMessage = "A quantidade é obrigatória.")]
    [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser maior que zero.")]
    public int Quantidade { get; set; }
}

