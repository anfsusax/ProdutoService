using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pedido.Domain.Entities;
using PedidoEntity = global::Pedido.Domain.Entities.PedidoModel;

namespace Pedido.Infrastructure.Persistence.Configurations;

public class PedidoConfiguration : IEntityTypeConfiguration<PedidoEntity>
{
    public void Configure(EntityTypeBuilder<PedidoEntity> builder)
    {
        builder.ToTable("pedidos");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.ValorTotal)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(p => p.DataPedido)
            .IsRequired();

        builder.Property(p => p.Status)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasMany(p => p.Itens)
            .WithOne()
            .HasForeignKey(i => i.PedidoId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

