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
                .HasMany(c => c.Itens)
                .WithOne(i => i.CustomerCart)
                .HasForeignKey(c => c.CustomerCartId);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys())) 
                relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
                       
        }
    }
}
