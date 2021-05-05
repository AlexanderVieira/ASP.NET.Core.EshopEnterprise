using ESE.Core.Mediator;
using ESE.Core.Mediator.Interfaces;
using ESE.Order.API.Application.Interfaces;
using ESE.Order.API.Application.Queries;
using ESE.Order.Domain.Model.Interfaces;
using ESE.Order.Domain.Vouchers.Inferfaces;
using ESE.Order.Infra.Data;
using ESE.Order.Infra.Data.Repository;
using ESE.WebAPI.Core.AspNetUser;
using ESE.WebAPI.Core.AspNetUser.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;


namespace ESE.Order.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            // API
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAspNetUser, AspNetUser>();
            
            // Commands
            //services.AddScoped<IRequestHandler<AddOrderCommand, ValidationResult>, OrderCommandHandler>();

            // Events
            //services.AddScoped<INotificationHandler<OrderEventPlaced>, OrderEventHandler>();

            // Application
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddScoped<IVoucherQueries, VoucherQueries>();
            //services.AddScoped<IOrderQueries, OrderQueries>();

            // Data
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IVoucherRepository, VoucherRepository>();
            services.AddScoped<OrderContext>();
        }
    }
}