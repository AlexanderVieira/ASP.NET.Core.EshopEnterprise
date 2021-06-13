using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using ESE.Porchase.API.Models;
using ESE.Porchase.API.Services.Interfaces;
using ESE.WebAPI.Core.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESE.Porchase.API.Controllers
{
    [Authorize]
    public class OrderController : BaseController
    {
        private readonly ICustomerCartService _customerCartService;
        private readonly ICatalogService _catalogService;
        private readonly IOrderService _orderService;
        private readonly IClientService _clientService;

        public OrderController(
            ICustomerCartService customerCartService, 
            ICatalogService catalogService, 
            IOrderService orderService, 
            IClientService clientService)
        {
            _customerCartService = customerCartService;
            _catalogService = catalogService;
            _orderService = orderService;
            _clientService = clientService;
        }
        
        [HttpPost]
        [Route("porchases/order")]
        public async Task<IActionResult> AddOrder(OrderDTO order)
        {
            var customerCart = await _customerCartService.GetCustomerCart();
            var products = await _catalogService.GetAll(customerCart.Items.Select(p => p.ProductId));
            var address = await _clientService.GetAddress();
            if (!await ValidCartProducts(customerCart, products)) return CustomResponse();
            SeedDataOrder(customerCart, address, order);
            return CustomResponse(await _orderService.Checkout(order));
        }

        [HttpGet]
        [Route("porchases/order/last")]
        public async Task<IActionResult> LastOrder()
        {
            var order = await _orderService.GetLastOrder();
            if (order is null)
            {
                AddProcessingErrors("Pedido não encontrado!");
                return CustomResponse();
            }
            return CustomResponse(order);
        }

        [HttpGet]
        [Route("porchases/order/list-client")]
        public async Task<IActionResult> ListByCustomer()
        {
            var orders = await _orderService.GetListByClientId();
            return orders == null ? NotFound() : CustomResponse(orders);
        }     

        private async Task<bool> ValidCartProducts(CustomerCartDTO customerCart, IEnumerable<ItemProductDTO> products)
        {
            if (customerCart.Items.Count != products.Count())
            {
                var unavailableItems = customerCart.Items.Select(x => x.ProductId).Except(products.Select(p => p.ItemId)).ToList();
                foreach (var itemId in unavailableItems)
                {
                    var itemCart = customerCart.Items.FirstOrDefault(c => c.ProductId == itemId);
                    AddProcessingErrors($"O item {itemCart.Name} não está mais disponível no catálogo, o remova do customerCart para prosseguir com a compra");
                }
                return false;
            }

            foreach (var itemCart in customerCart.Items)
            {
                var catalogProduct = products.FirstOrDefault(p => p.ItemId == itemCart.ProductId);
                if (catalogProduct.Value != itemCart.Value)
                {
                    var msgError = $"O produto {itemCart.Name} mudou de valor (de: " +
                                   $"{string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", itemCart.Value)} para: " +
                                   $"{string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", catalogProduct.Value)}) desde que foi adicionado ao customerCart.";

                    AddProcessingErrors(msgError);

                    var responseRemove = await _customerCartService.RemoveItemCart(itemCart.ProductId);
                    if (ResponseHasErrors(responseRemove))
                    {
                        AddProcessingErrors($"Não foi possível remover automaticamente o produto {itemCart.Name} do seu customerCart, _" +
                                                   "remova e adicione novamente caso ainda deseje comprar este item");
                        return false;
                    }

                    itemCart.Value = catalogProduct.Value;
                    var addResponse = await _customerCartService.AddItemCart(itemCart);

                    if (ResponseHasErrors(addResponse))
                    {
                        AddProcessingErrors($"Não foi possível atualizar automaticamente o produto {itemCart.Name} do seu customerCart, _" +
                                                   "adicione novamente caso ainda deseje comprar este item");
                    }

                    ClearProcessingErrors();
                    AddProcessingErrors(msgError + " Atualizamos o valor em seu customerCart, realize a conferência do order e se preferir remova o produto");
                    return false;
                }
            }
            return true;
        }
        
        private void SeedDataOrder(CustomerCartDTO customerCart, AddressDTO address, OrderDTO order)
        {
            order.VoucherCode = customerCart.Voucher?.Code;
            order.VoucherUsed = customerCart.VoucherUsed;
            order.TotalValue = customerCart.TotalValue;
            order.Discount = customerCart.Discount;
            order.OrderItems = customerCart.Items;

            order.Address = address;
        }
    }
}
