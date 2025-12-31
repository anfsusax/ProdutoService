using PedidoEntity = global::Pedido.Domain.Entities.PedidoModel;

namespace Pedido.Application.Interfaces;

public interface IPedidoRepository
{
    Task AdicionarAsync(PedidoEntity pedido);
    Task<PedidoEntity?> ObterPorIdAsync(Guid id);
    Task<IEnumerable<PedidoEntity>> ObterTodosAsync();
}
