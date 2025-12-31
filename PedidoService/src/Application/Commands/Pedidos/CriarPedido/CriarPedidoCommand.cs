namespace Pedido.Application.Commands.Pedidos.CriarPedido;

public record CriarPedidoCommand(List<CriarPedidoItemDto> Itens);

public record CriarPedidoItemDto(Guid ProdutoId, int Quantidade);
