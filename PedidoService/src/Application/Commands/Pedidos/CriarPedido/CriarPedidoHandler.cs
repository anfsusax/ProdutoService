using Pedido.Application.Interfaces;
using PedidoEntity = global::Pedido.Domain.Entities.PedidoModel;

namespace Pedido.Application.Commands.Pedidos.CriarPedido;

public class CriarPedidoHandler
{
    private readonly IPedidoRepository _pedidoRepository;

    public CriarPedidoHandler(IPedidoRepository pedidoRepository)
    {
        _pedidoRepository = pedidoRepository;
    }

    public async Task<Guid> Handle(CriarPedidoCommand command)
    {
        var pedido = new PedidoEntity(command.ValorTotal);
        await _pedidoRepository.AdicionarAsync(pedido);
        return pedido.Id;
    }
}
