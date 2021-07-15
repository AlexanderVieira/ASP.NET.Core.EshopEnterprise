using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ESE.Order.Infra.Data.Mapping
{
    public class OrderMapping : IEntityTypeConfiguration<Domain.Model.Order>
    {
        public void Configure(EntityTypeBuilder<Domain.Model.Order> builder)
        {
            builder.HasKey(c => c.Id);

            builder.OwnsOne(p => p.Address, e =>
            {
                e.Property(pe => pe.Street)
                    .HasColumnName("Street");

                e.Property(pe => pe.Number)
                    .HasColumnName("Number");

                e.Property(pe => pe.Complement)
                    .HasColumnName("Complement");

                e.Property(pe => pe.District)
                    .HasColumnName("District");

                e.Property(pe => pe.CodePostal)
                    .HasColumnName("CodePostal");

                e.Property(pe => pe.City)
                    .HasColumnName("City");

                e.Property(pe => pe.State)
                    .HasColumnName("State");
            });

            builder.Property(c => c.Discount)
                .HasColumnType("decimal(18,2)");

            builder.Property(c => c.TotalValue)
                .HasColumnType("decimal(18,2)");

            builder.Property(c => c.Code)
                .HasColumnName("Code")
                .HasDefaultValueSql("NEXT VALUE FOR MinhaSequencia");

            // 1 : N => Pedido : PedidoItems
            builder.HasMany(c => c.OrderItems)
                .WithOne(c => c.Order)
                .HasForeignKey(c => c.OrderId);

            builder.ToTable("Orders");
        }
    }
}
