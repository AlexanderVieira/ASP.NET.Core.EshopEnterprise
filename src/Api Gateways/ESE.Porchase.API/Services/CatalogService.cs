using ESE.Porchase.API.Extensions;
using ESE.Porchase.API.Models;
using ESE.Porchase.API.Services.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ESE.Porchase.API.Services
{
    public class CatalogService : TextSerializerService, ICatalogService
    {
        private readonly HttpClient _httpClient;

        public CatalogService(HttpClient httpClient, IOptions<AppSettings> settings)
        {            
            httpClient.BaseAddress = new Uri("https://localhost:44364");
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<ItemProductDTO>> GetAll(IEnumerable<Guid> ids)
        {
            var idsRequest = string.Join(",", ids);
            var response = await _httpClient.GetAsync($"/catalog/products/{idsRequest}");

            HandlerResponseErrors(response);

            return await DeserializeResponseObject<IEnumerable<ItemProductDTO>>(response);
        }

        public async Task<ItemProductDTO> GetById(Guid id)
        {            
            var response = await _httpClient.GetAsync($"/catalog/products/{id}");

            HandlerResponseErrors(response);

            return await DeserializeResponseObject<ItemProductDTO>(response);
        }

    }
}
