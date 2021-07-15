using ESE.Order.Domain.Vouchers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ESE.Order.Infra.Data.Mapping
{
    public class VoucherMapping : IEntityTypeConfiguration<Voucher>
    {
        public void Configure(EntityTypeBuilder<Voucher> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.VoucherCode)
                .IsRequired()
                .HasColumnName("VoucherCode")
                .HasColumnType("varchar(100)");

            builder.Property(c => c.Percentage)                
                .HasColumnType("decimal(18,2)");

            builder.Property(c => c.TotalDiscount)
                .HasColumnType("decimal(18,2)");

            builder.ToTable("Vouchers");
        }
    }
}
