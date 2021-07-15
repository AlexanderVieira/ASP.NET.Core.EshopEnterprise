using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ESE.Porchase.API.Configuration
{
    public static class GrpcConfig
    {
        public static void ConfigureGrpcServices(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddSingleton<GrpcServiceInterceptor>();

            //services.AddScoped<ICarrinhoGrpcService, CarrinhoGrpcService>();

            //services.AddGrpcClient<CarrinhoCompras.CarrinhoComprasClient>(options =>
            //{
            //    options.Address = new Uri(configuration["CarrinhoUrl"]);
            //})
            //    .AddInterceptor<GrpcServiceInterceptor>()
            //    .AllowSelfSignedCertificate();
        }
    }
}
