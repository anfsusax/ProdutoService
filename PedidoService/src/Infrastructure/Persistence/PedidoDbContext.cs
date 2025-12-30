using Microsoft.EntityFrameworkCore;
using PedidoEntity = global::Pedido.Domain.Entities.PedidoModel;
using Pedido.Domain.Entities;

namespace Pedido.Infrastructure.Persistence;

public class PedidoDbContext : DbContext
{
    public PedidoDbContext(DbContextOptions<PedidoDbContext> options)
        : base(options)
    {
    }

    public DbSet<PedidoEntity> Pedidos => Set<PedidoEntity>();
    public DbSet<PedidoItem> PedidoItens => Set<PedidoItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PedidoDbContext).Assembly);
    }
}
