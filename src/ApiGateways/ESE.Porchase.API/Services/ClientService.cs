using ESE.Porchase.API.Models;
using ESE.Porchase.API.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ESE.Porchase.API.Services
{
    public class ClientService : TextSerializerService, IClientService
    {
        private readonly HttpClient _httpClient;
        private const string URL_KEY = "ClientUrl";

        public ClientService(HttpClient httpClient, IConfiguration configuration)
        {            
            httpClient.BaseAddress = new Uri(configuration[URL_KEY]);
            _httpClient = httpClient;
        }

        public async Task<AddressDTO> GetAddress()
        {
            var response = await _httpClient.GetAsync("/client/address/");

            if (response.StatusCode == HttpStatusCode.NotFound) return null;

            HandlerResponseErrors(response);

            return await DeserializeResponseObject<AddressDTO>(response);
        }
    }
}
