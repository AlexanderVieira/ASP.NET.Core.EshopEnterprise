using ESE.WebAPI.Core.AspNetUser;
using ESE.WebAPI.Core.AspNetUser.Interfaces;
using ESE.WebAPI.Core.Auth;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NetDevPack.Security.JwtSigningCredentials.AspNetCore;

namespace ESE.Auth.API.Configuration
{
    public static class ApiConfig
    {
         public static IServiceCollection AddApiConfiguration(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddScoped<IAspNetUser, AspNetUser>();
            return services;
        }

        public static IApplicationBuilder UseApiConfiguration(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthConfiguration();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseJwksDiscovery();

            return app;
        }
    }
}