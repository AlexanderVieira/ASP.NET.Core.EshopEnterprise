using ESE.Client.API.Application.Commands;
using ESE.Client.API.Application.Events;
using ESE.Client.API.Data;
using ESE.Client.API.Data.Repository;
using ESE.Client.API.Models.Interfaces;
using ESE.Core.Mediator;
using ESE.Core.Mediator.Interfaces;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace ESE.Client.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddScoped<IRequestHandler<RegisterCustomerCommand, ValidationResult>, CustomerCommandHandler>();
            services.AddScoped<INotificationHandler<RegisteredCustomerEvent>, CustomerEventHandler>();

            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<CustomerContext>();
        }
    }
}
