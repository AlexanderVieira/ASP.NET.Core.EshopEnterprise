using ESE.Store.MVC.Extensions;
using ESE.Store.MVC.Models;
using ESE.Store.MVC.Services.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ESE.Store.MVC.Services
{
    public class CustomerCartService : TextSerializerService, ICustomerCartService
    {
        private readonly HttpClient _httpClient;

        public CustomerCartService(HttpClient httpClient, IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.ShoppingCartUrl);
        }

        public async Task<CustomerCartViewModel> GetCustomerCart()
        {
            var response = await _httpClient.GetAsync("/cart/");
            HandlerResponseErrors(response);
            return await DeserializeResponseObject<CustomerCartViewModel>(response);
        }

        public async Task<ResponseResult> AddItemCart(ItemCartViewModel item)
        {
            var itemContent = GetContent(item);
            var response = await _httpClient.PostAsync("/cart/", itemContent);
            if (!HandlerResponseErrors(response))
            {
                return await DeserializeResponseObject<ResponseResult>(response);
            }

            return ReturnOk();
        }        

        public async Task<ResponseResult> RemoveItemCart(Guid productId)
        {
            var response = await _httpClient.DeleteAsync($"/cart/{productId}");
            if (!HandlerResponseErrors(response))
            {
                return await DeserializeResponseObject<ResponseResult>(response);
            }

            return ReturnOk();
        }

        public async Task<ResponseResult> UpdateItemCart(Guid productId, ItemCartViewModel item)
        {
            var itemContent = GetContent(item);
            var response = await _httpClient.PutAsync($"/cart/{item.ProductId}", itemContent);
            if (!HandlerResponseErrors(response))
            {
                return await DeserializeResponseObject<ResponseResult>(response);
            }

            return ReturnOk();
        }
    }
}
