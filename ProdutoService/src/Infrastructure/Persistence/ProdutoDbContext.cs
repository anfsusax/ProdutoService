using Infrastructure.Outbox;
using Microsoft.EntityFrameworkCore;
 

namespace Infrastructure.Persistence;

public class ProdutoDbContext : DbContext
{
    public ProdutoDbContext(DbContextOptions<ProdutoDbContext> options)
        : base(options)
    {
    }

    public DbSet<Domain.Entities.Produto> Produtos => Set<Domain.Entities.Produto>();
    public DbSet<OutboxEvent> OutboxEvents => Set<OutboxEvent>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProdutoDbContext).Assembly);
    }
}
