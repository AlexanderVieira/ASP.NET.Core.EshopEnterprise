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
    public class PorchasesBffService : TextSerializerService, IPorchasesBffService
    {
        private readonly HttpClient _httpClient;

        public PorchasesBffService(HttpClient httpClient, IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.PorchaseBffUrl);
        }

        #region CustomerCart
        public async Task<CustomerCartViewModel> GetCustomerCart()
        {
            var response = await _httpClient.GetAsync("/porchases/cart/");
            HandlerResponseErrors(response);
            return await DeserializeResponseObject<CustomerCartViewModel>(response);
        }

        public async Task<int> GetQuantityCustomerCart()
        {
            var response = await _httpClient.GetAsync("/porchases/cart-quantity/");
            HandlerResponseErrors(response);
            return await DeserializeResponseObject<int>(response);
        }

        public async Task<ResponseResult> AddItemCart(ItemCartViewModel item)
        {
            var itemContent = GetContent(item);
            var response = await _httpClient.PostAsync("/porchases/cart/", itemContent);
            if (!HandlerResponseErrors(response))
            {
                return await DeserializeResponseObject<ResponseResult>(response);
            }

            return ReturnOk();
        }        

        public async Task<ResponseResult> RemoveItemCart(Guid productId)
        {
            var response = await _httpClient.DeleteAsync($"/porchases/cart/{productId}");
            if (!HandlerResponseErrors(response))
            {
                return await DeserializeResponseObject<ResponseResult>(response);
            }

            return ReturnOk();
        }

        public async Task<ResponseResult> UpdateItemCart(Guid productId, ItemCartViewModel item)
        {
            var itemContent = GetContent(item);
            var response = await _httpClient.PutAsync($"/porchases/cart/{item.ProductId}", itemContent);
            if (!HandlerResponseErrors(response))
            {
                return await DeserializeResponseObject<ResponseResult>(response);
            }

            return ReturnOk();
        }        

        public async Task<ResponseResult> ApplyVoucherCustomerCart(string voucher)
        {
            var itemContent = GetContent(voucher);
            var response = await _httpClient.PostAsync("/porchases/cart/apply-voucher/", itemContent);
            if (!HandlerResponseErrors(response)) 
            {
                return await DeserializeResponseObject<ResponseResult>(response);
            }                
            return ReturnOk();
        }

#endregion

        #region Order
        public async Task<ResponseResult> Checkout(OrderTransactionViewModel orderTransaction)
        {
            var orderContent = GetContent(orderTransaction);
            var response = await _httpClient.PostAsync("/porchases/order/", orderContent);
            if (!HandlerResponseErrors(response))
            {
                return await DeserializeResponseObject<ResponseResult>(response);
            }

            return ReturnOk();
        }

        public async Task<OrderViewModel> GetLastOrder()
        {
            var response = await _httpClient.GetAsync("/porchases/order/last/");
            HandlerResponseErrors(response);
            return await DeserializeResponseObject<OrderViewModel>(response);
        }

        public async Task<IEnumerable<OrderViewModel>> GetListByClientId()
        {
            var response = await _httpClient.GetAsync("/order/list-client/");            
            HandlerResponseErrors(response);
            return await DeserializeResponseObject<IEnumerable<OrderViewModel>>(response);
        }

        public OrderTransactionViewModel MapToOrder(CustomerCartViewModel customerCart, AddressViewModel address)
        {
            var order = new OrderTransactionViewModel
            {
                TotalValue = customerCart.TotalValue,
                OrderItems = customerCart.Items,
                Discount = customerCart.Discount,
                VoucherUsed = customerCart.VoucherUsed,
                VoucherCode = customerCart.Voucher?.Code
            };

            if (address != null)
            {
                order.Address = new AddressViewModel
                {
                    Street = address.Street,
                    Number = address.Number,
                    District = address.District,
                    CodePostal = address.CodePostal,
                    Complement = address.Complement,
                    City = address.City,
                    State = address.State
                };
            }

            return order;
        }

        #endregion
    }
}
