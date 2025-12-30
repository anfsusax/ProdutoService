using Microsoft.EntityFrameworkCore;
using PedidoEntity = global::Pedido.Domain.Entities.PedidoModel;

namespace Pedido.Infrastructure.Persistence;

public class PedidoDbContext : DbContext
{
    public PedidoDbContext(DbContextOptions<PedidoDbContext> options)
        : base(options)
    {
    }

    public DbSet<PedidoEntity> Pedidos => Set<PedidoEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PedidoDbContext).Assembly);
    }
}
