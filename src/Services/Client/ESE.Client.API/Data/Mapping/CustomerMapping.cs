using ESE.Client.API.Models;
using ESE.Core.DomainObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ESE.Client.API.Data.Mapping
{
    public class CustomerMapping : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.OwnsOne(c => c.Cpf, tf =>
            {
                tf.Property(c => c.Number)
                .IsRequired()
                .HasMaxLength(Cpf.CPF_MAX_LENGTH)
                .HasColumnName("Cpf")
                .HasColumnType($"varchar({Cpf.CPF_MAX_LENGTH})");
            });

            builder.OwnsOne(c => c.Email, tf =>
            {
                tf.Property(c => c.Address)
                .IsRequired()
                .HasColumnName("Email")
                .HasColumnType($"varchar({Email.ADDRESS_MAX_LENGTH})");
            });

            builder.HasOne(c => c.Address)
                .WithOne(c => c.Customer);                

            builder.ToTable("Customers");
        }
    }
}
