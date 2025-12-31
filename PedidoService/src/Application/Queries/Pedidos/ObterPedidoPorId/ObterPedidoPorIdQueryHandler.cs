using Pedido.Application.Interfaces;
using PedidoEntity = global::Pedido.Domain.Entities.PedidoModel;

namespace Pedido.Application.Queries.Pedidos.ObterPedidoPorId;

public class ObterPedidoPorIdQueryHandler
{
    private readonly IPedidoRepository _pedidoRepository;

    public ObterPedidoPorIdQueryHandler(IPedidoRepository pedidoRepository)
    {
        _pedidoRepository = pedidoRepository;
    }

    public async Task<PedidoEntity?> HandleAsync(ObterPedidoPorIdQuery query, CancellationToken cancellationToken = default)
    {
        return await _pedidoRepository.ObterPorIdAsync(query.Id);
    }
}

