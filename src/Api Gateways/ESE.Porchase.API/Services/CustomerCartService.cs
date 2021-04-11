using ESE.Porchase.API.Extensions;
using ESE.Porchase.API.Services.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;

namespace ESE.Porchase.API.Services
{
    public class CustomerCartService : TextSerializerService, ICustomerCartService
    {
        private readonly HttpClient _httpClient;

        public CustomerCartService(HttpClient httpClient, IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.ShoppingCartUrl);
        }

    }
}
