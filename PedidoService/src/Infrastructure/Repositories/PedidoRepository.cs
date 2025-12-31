using Microsoft.EntityFrameworkCore;
using Pedido.Application.Interfaces;
using Pedido.Infrastructure.Persistence;
using PedidoEntity = global::Pedido.Domain.Entities.PedidoModel;

namespace Pedido.Infrastructure.Repositories;

public class PedidoRepository : IPedidoRepository
{
    private readonly PedidoDbContext _context;

    public PedidoRepository(PedidoDbContext context)
    {
        _context = context;
    }

    public async Task AdicionarAsync(PedidoEntity pedido)
    {
        await _context.Pedidos.AddAsync(pedido);
        await _context.SaveChangesAsync();
    }

    public async Task<PedidoEntity?> ObterPorIdAsync(Guid id)
    {
        return await _context.Pedidos
            .Include(p => p.Itens)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<PedidoEntity>> ObterTodosAsync()
    {
        return await _context.Pedidos
            .Include(p => p.Itens)
            .OrderByDescending(p => p.DataPedido)
            .ToListAsync();
    }
}
