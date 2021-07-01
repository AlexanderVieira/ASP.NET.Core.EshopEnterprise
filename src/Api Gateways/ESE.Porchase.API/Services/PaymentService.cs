using ESE.Porchase.API.Extensions;
using ESE.Porchase.API.Services.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;

namespace ESE.Porchase.API.Services
{
    public class PaymentService : TextSerializerService, IPaymentService
    {
        private readonly HttpClient _httpClient;

        public PaymentService(HttpClient httpClient, IOptions<AppSettings> settings)
        {            
            httpClient.BaseAddress = new Uri("https://localhost:44335");
            _httpClient = httpClient;
        }
        
    }
}
