using Pedido.Application.Interfaces;
using PedidoEntity = global::Pedido.Domain.Entities.PedidoModel;

namespace Pedido.Application.Queries.Pedidos.ObterTodosPedidos;

public class ObterTodosPedidosQueryHandler
{
    private readonly IPedidoRepository _pedidoRepository;

    public ObterTodosPedidosQueryHandler(IPedidoRepository pedidoRepository)
    {
        _pedidoRepository = pedidoRepository;
    }

    public async Task<IEnumerable<PedidoEntity>> HandleAsync(CancellationToken cancellationToken = default)
    {
        return await _pedidoRepository.ObterTodosAsync();
    }
}

