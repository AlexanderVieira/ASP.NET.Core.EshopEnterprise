using ESE.Porchase.API.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;

namespace ESE.Porchase.API.Services
{
    public class PaymentService : TextSerializerService, IPaymentService
    {
        private readonly HttpClient _httpClient;
        private const string URL_KEY = "PaymentUrl";

        public PaymentService(HttpClient httpClient, IConfiguration configuration)
        {            
            httpClient.BaseAddress = new Uri(configuration[URL_KEY]);
            _httpClient = httpClient;
        }
        
    }
}
