using ESE.Store.MVC.Extensions;
using ESE.Store.MVC.Models;
using ESE.Store.MVC.Services.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ESE.Store.MVC.Services
{
    public class CatalogService : TextSerializerService, ICatalogService
    {
        private readonly HttpClient _httpClient;

        public CatalogService(HttpClient httpClient, IOptions<AppSettings> settings)
        {
            httpClient.BaseAddress = new Uri(settings.Value.CatalogUrl);
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<ProductViewModel>> GetAll()
        {
            var response = await _httpClient.GetAsync("/catalog/products/");

            HandlerResponseErrors(response);

            return await DeserializeResponseObject<IEnumerable<ProductViewModel>>(response);
        }

        public async Task<ProductViewModel> GetById(Guid id)
        {
            var response = await _httpClient.GetAsync($"/catalog/products/{id}");

            HandlerResponseErrors(response);

            return await DeserializeResponseObject<ProductViewModel>(response);
        }
    }
}
