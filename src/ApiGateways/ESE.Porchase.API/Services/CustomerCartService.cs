using ESE.Core.Comunication;
using ESE.Porchase.API.Models;
using ESE.Porchase.API.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ESE.Porchase.API.Services
{
    public class CustomerCartService : TextSerializerService, ICustomerCartService
    {
        private readonly HttpClient _httpClient;
        private const string URL_KEY = "ShoppingCartUrl";

        public CustomerCartService(HttpClient httpClient, IConfiguration configuration)
        {            
            httpClient.BaseAddress = new Uri(configuration[URL_KEY]);
            _httpClient = httpClient;
        }

        public async Task<CustomerCartDTO> GetCustomerCart()
        {
            var response = await _httpClient.GetAsync("/cart/");
            HandlerResponseErrors(response);
            return await DeserializeResponseObject<CustomerCartDTO>(response);
        }

        public async Task<ResponseResult> AddItemCart(ItemCartDTO item)
        {
            var itemContent = GetContent(item);
            var response = await _httpClient.PostAsync("/cart/items/add-item/", itemContent);
            if (!HandlerResponseErrors(response))
            {
                return await DeserializeResponseObject<ResponseResult>(response);
            }

            return ReturnOk();
        }

        public async Task<ResponseResult> RemoveItemCart(Guid productId)
        {
            var response = await _httpClient.DeleteAsync($"/cart/items/remove-item/{productId}");
            if (!HandlerResponseErrors(response))
            {
                return await DeserializeResponseObject<ResponseResult>(response);
            }

            return ReturnOk();
        }

        public async Task<ResponseResult> UpdateItemCart(Guid productId, ItemCartDTO item)
        {
            var itemContent = GetContent(item);
            var response = await _httpClient.PutAsync($"/cart/items/update-item/{item.ProductId}", itemContent);
            if (!HandlerResponseErrors(response))
            {
                return await DeserializeResponseObject<ResponseResult>(response);
            }

            return ReturnOk();
        }

        public async Task<ResponseResult> ApplyVoucherCart(VoucherDTO voucher)
        {
            var itemContent = GetContent(voucher);

            var response = await _httpClient.PostAsync("/cart/apply-voucher/", itemContent);

            if (!HandlerResponseErrors(response)) return await DeserializeResponseObject<ResponseResult>(response);

            return ReturnOk();
        }
    }
}
