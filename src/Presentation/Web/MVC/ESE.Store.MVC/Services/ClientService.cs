using ESE.Store.MVC.Extensions;
using ESE.Store.MVC.Models;
using ESE.Store.MVC.Services.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ESE.Store.MVC.Services
{
    public class ClientService : TextSerializerService, IClientService
    {
        private readonly HttpClient _httpClient;

        public ClientService(HttpClient httpClient, IOptions<AppSettings> settings)
        {
            httpClient.BaseAddress = new Uri(settings.Value.ClientUrl);
            _httpClient = httpClient;
        }

        public async Task<ResponseResult> AddAddress(AddressViewModel address)
        {
            var addressContent = GetContent(address);
            var response = await _httpClient.PostAsync("/client/address/", addressContent);
            HandlerResponseErrors(response);
            return await DeserializeResponseObject<ResponseResult>(response);
        }

        public async Task<AddressViewModel> GetAddress()
        {
            var response = await _httpClient.GetAsync("/client/address/");
            if (response.StatusCode == HttpStatusCode.NotFound) return null;
            HandlerResponseErrors(response);
            return await DeserializeResponseObject<AddressViewModel>(response);
        }
                
    }
}
