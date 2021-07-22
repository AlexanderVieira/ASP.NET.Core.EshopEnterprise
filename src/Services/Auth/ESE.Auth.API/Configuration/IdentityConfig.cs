using ESE.Auth.API.Data;
using ESE.Auth.API.Extensions;
using ESE.WebAPI.Core.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetDevPack.Security.JwtSigningCredentials;

namespace ESE.Auth.API.Configuration
{
    public static class IdentityConfig
    {
        public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services, IConfiguration configuration){

            var appSettingsSection = configuration.GetSection("AppTokenSettings");
            services.Configure<AppTokenSettings>(appSettingsSection);

            services.AddJwksManager(options => options.Algorithm = Algorithm.ES256)
                .PersistKeysToDatabaseStore<AuthDbContext>();

            services.AddDbContext<AuthDbContext>(options => 
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), 
                                     providerOptions => providerOptions.EnableRetryOnFailure()));

            services.AddDefaultIdentity<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddErrorDescriber<IdentityMessagesPtBr>()
                .AddEntityFrameworkStores<AuthDbContext>()
                .AddDefaultTokenProviders();            

            return services;
        }      
                
    }
}