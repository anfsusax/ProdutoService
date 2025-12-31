using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pedido.Domain.Entities;

namespace Pedido.Infrastructure.Persistence.Configurations;

public class PedidoItemConfiguration : IEntityTypeConfiguration<PedidoItem>
{
    public void Configure(EntityTypeBuilder<PedidoItem> builder)
    {
        builder.ToTable("pedido_itens");

        builder.HasKey(i => i.Id);

        builder.Property(i => i.PedidoId)
            .IsRequired();

        builder.Property(i => i.ProdutoId)
            .IsRequired();

        builder.Property(i => i.Nome)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(i => i.Quantidade)
            .IsRequired();

        builder.Property(i => i.PrecoUnitario)
            .IsRequired()
            .HasPrecision(18, 2);
    }
}

