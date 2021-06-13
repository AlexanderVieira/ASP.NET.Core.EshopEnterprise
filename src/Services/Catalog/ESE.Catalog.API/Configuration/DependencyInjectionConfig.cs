using ESE.Catalog.API.Data;
using ESE.Catalog.API.Data.Repository;
using ESE.Catalog.API.Models.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ESE.Catalog.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<CatalogContext>();
        }
    }
}
