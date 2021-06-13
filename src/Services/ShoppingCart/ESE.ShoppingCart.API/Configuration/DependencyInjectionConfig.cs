using ESE.ShoppingCart.API.Data;
using ESE.WebAPI.Core.AspNetUser;
using ESE.WebAPI.Core.AspNetUser.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace ESE.ShoppingCart.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAspNetUser, AspNetUser>();
            services.AddScoped<ShoppingCartContext>();
        }
    }
}
