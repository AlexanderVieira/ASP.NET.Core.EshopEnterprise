using ESE.Core.Comunication;
using ESE.Porchase.API.Models;
using ESE.Porchase.API.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ESE.Porchase.API.Services
{
    public class OrderService : TextSerializerService, IOrderService
    {
        private readonly HttpClient _httpClient;
        private const string URL_KEY = "OrderUrl";

        public OrderService(HttpClient httpClient, IConfiguration configuration)
        {            
            httpClient.BaseAddress = new Uri(configuration[URL_KEY]);
            _httpClient = httpClient;
        }

        public async Task<ResponseResult> Checkout(OrderDTO order)
        {
            var orderContent = GetContent(order);

            var response = await _httpClient.PostAsync("/order/", orderContent);
            
            if (!HandlerResponseErrors(response))
            {
                return await DeserializeResponseObject<ResponseResult>(response);
            }

            return ReturnOk();
        }

        public async Task<OrderDTO> GetLastOrder()
        {
            var response = await _httpClient.GetAsync("/order/last/");

            if (response.StatusCode == HttpStatusCode.NotFound) return null;

            HandlerResponseErrors(response);

            return await DeserializeResponseObject<OrderDTO>(response);
        }

        public async Task<IEnumerable<OrderDTO>> GetListByClientId()
        {
            var response = await _httpClient.GetAsync("/order/list-client/");

            if (response.StatusCode == HttpStatusCode.NotFound) return null;

            HandlerResponseErrors(response);

            return await DeserializeResponseObject<IEnumerable<OrderDTO>>(response);
        }

        public async Task<VoucherDTO> GetVoucherByCode(string code)
        {
            var response = await _httpClient.GetAsync($"/voucher/{code}/");

            if (response.StatusCode == HttpStatusCode.NotFound) return null;

            HandlerResponseErrors(response);

            return await DeserializeResponseObject<VoucherDTO>(response);
        }
    }
}
