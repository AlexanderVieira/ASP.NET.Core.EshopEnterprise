using ESE.Client.API.Application.Commands;
using ESE.Core.Mediator.Interfaces;
using ESE.Core.Messages.Integration;
using ESE.MessageBus.Interfaces;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ESE.Client.API.Services
{
    public class RegisterCustomerIntegrationHandler : BackgroundService
    {
        private readonly IMessageBus _bus;
        private readonly IServiceProvider _serviceProvider;

        public RegisterCustomerIntegrationHandler(IMessageBus bus, IServiceProvider serviceProvider)
        {
            _bus = bus;
            _serviceProvider = serviceProvider;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            SetResponder();
            return Task.CompletedTask;
        }

        private void SetResponder()
        {
            _bus.RespondAsync<RegisteredUserIntegrationEvent, ResponseMessage>(async message => await CustomerRecord(message));
            _bus.AdvancedBus.Connected += OnConnect;
        }

        private async Task<ResponseMessage> CustomerRecord(RegisteredUserIntegrationEvent message)
        {
            var customerCommand = new RegisterCustomerCommand(message.Id, message.Name, message.Email, message.Cpf);
            ValidationResult sucesso;

            using (var scope = _serviceProvider.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();
                sucesso = await mediator.SendCommand(customerCommand);
            }

            return new ResponseMessage(sucesso);
        }

        private void OnConnect(object sender, EventArgs e)
        {
            SetResponder();
        }
    }
}
