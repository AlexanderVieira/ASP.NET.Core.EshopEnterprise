using ESE.Client.API.Extensions;
using ESE.Client.API.Models;
using ESE.Core.Data.Interfaces;
using ESE.Core.Mediator.Interfaces;
using ESE.Core.Messages;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ESE.Client.API.Data
{
    public class CustomerContext : DbContext, IUnitOfWork
    {
        private readonly IMediatorHandler _mediatorHandler;
        public DbSet<Customer>  Customers { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public CustomerContext(DbContextOptions<CustomerContext> options, IMediatorHandler mediatorHandler) : base(options)
        {
            _mediatorHandler = mediatorHandler;
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<ValidationResult>();
            modelBuilder.Ignore<Event>();

            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CustomerContext).Assembly);
        }
        public async Task<bool> Commit()
        {
            var success = await base.SaveChangesAsync() > 0;
            if (success) await _mediatorHandler.PublishEvents(this);
            return success;
        }
    }
}
