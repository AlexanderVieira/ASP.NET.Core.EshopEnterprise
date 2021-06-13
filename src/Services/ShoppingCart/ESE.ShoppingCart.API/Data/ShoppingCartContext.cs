using ESE.ShoppingCart.API.Models;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ESE.ShoppingCart.API.Data
{
    public sealed class ShoppingCartContext : DbContext
    {
        public DbSet<CustomerCart>  CustomerCarts { get; set; }
        public DbSet<ItemCart> ItemCarts { get; set; }
        public ShoppingCartContext(DbContextOptions<ShoppingCartContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetProperties()
                .Where(p => p.ClrType == typeof(string)))) 
                property.SetColumnType("varchar(100)");

            modelBuilder.Ignore<ValidationResult>();

            modelBuilder.Entity<CustomerCart>()
                .HasIndex(c => c.CustomerId)
                .HasName("IDX_Customer");

            modelBuilder.Entity<CustomerCart>()
                .Property(c => c.Discount)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<CustomerCart>()
                .Property(c => c.TotalValue)
                .HasColumnType("decimal(18,2)");            

            modelBuilder.Entity<CustomerCart>()
                .Ignore(c => c.Voucher)
                .OwnsOne(c => c.Voucher, v =>
                {
                    v.Property(x => x.Code)
                    .HasColumnName("VoucherCode")
                    .HasColumnType("varchar(50)");

                    v.Property(x => x.DiscountType)
                    .HasColumnName("DiscountType");

                    v.Property(x => x.Percentage)
                    .HasColumnName("Percentage")
                    .HasColumnType("decimal(18,2)");

                    v.Property(x => x.TotalDiscount)
                    .HasColumnName("TotalDiscount")
                    .HasColumnType("decimal(18,2)");
                });

            modelBuilder.Entity<CustomerCart>()
                .HasMany(c => c.Items)
                .WithOne(i => i.CustomerCart)
                .HasForeignKey(c => c.CustomerCartId);

            modelBuilder.Entity<ItemCart>()
                .Property(c => c.Value)
                .HasColumnType("decimal(18,2)");

            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys())) 
                relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
                       
        }
    }
}
