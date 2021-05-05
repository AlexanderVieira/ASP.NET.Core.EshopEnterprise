using ESE.Core.Messages.Integration;
using ESE.MessageBus.Interfaces;
using ESE.ShoppingCart.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ESE.ShoppingCart.API.Services
{
    public class RegisterCustomerCartIntegrationHandler : BackgroundService
    {
        private readonly IMessageBus _bus;
        private readonly IServiceProvider _serviceProvider;

        public RegisterCustomerCartIntegrationHandler(IMessageBus bus, IServiceProvider serviceProvider)
        {
            _bus = bus;
            _serviceProvider = serviceProvider;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            SetSubscribers();
            return Task.CompletedTask;
        }

        private void SetSubscribers()
        {
            _bus.SubscribeAsync<OrderPlacedIntegrationEvent>("PedidoRealizado", async request => await ClearCustomerCart(request));
        }

        private async Task ClearCustomerCart(OrderPlacedIntegrationEvent message)
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ShoppingCartContext>();

            var customerCart = await context.CustomerCarts
                .FirstOrDefaultAsync(c => c.CustomerId == message.CustomerId);

            if (customerCart != null)
            {
                context.CustomerCarts.Remove(customerCart);
                await context.SaveChangesAsync();
            }
        }
    }
}
