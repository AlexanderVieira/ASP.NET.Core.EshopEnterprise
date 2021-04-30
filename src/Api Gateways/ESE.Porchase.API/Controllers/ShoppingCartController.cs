using System;
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
    public class ShoppingCartController : BaseController
    {
        private readonly ICustomerCartService _customerCartService;
        private readonly ICatalogService _catalogService;
        private readonly IOrderService _orderService;

        public ShoppingCartController(
            ICustomerCartService customerCartService, 
            ICatalogService catalogService, 
            IOrderService orderService)
        {
            _customerCartService = customerCartService;
            _catalogService = catalogService;
            _orderService = orderService;
        }

        [HttpGet]
        [Route("porchases/cart")]
        public async Task<IActionResult> Index()
        {
            return CustomResponse(await _customerCartService.GetCustomerCart());
        }

        [HttpGet]
        [Route("porchases/cart-quantity")]
        public async Task<int> GetCartQuantity()
        {
            var quantity = await _customerCartService.GetCustomerCart();
            return quantity?.Items.Sum(i => i.Quantity) ?? 0;
        }

        [HttpPost]
        [Route("porchases/cart/items")]
        public async Task<IActionResult> AddItemCart(ItemCartDTO itemCart)
        {
            var product = await _catalogService.GetById(itemCart.ProductId);
            await ValidItemCart(product, itemCart.Quantity, true);
            if (!ValidOperation()) return CustomResponse();
            itemCart.Name = product.Name;
            itemCart.Value = product.Value;
            itemCart.Image = product.Image;
            var response = await _customerCartService.AddItemCart(itemCart);
            return CustomResponse(response);
        }

        [HttpPut]
        [Route("porchases/cart/items/{productId}")]
        public async Task<IActionResult> UpdateItemCart(Guid productId, ItemCartDTO itemCart)
        {
            var product = await _catalogService.GetById(productId);
            await ValidItemCart(product, itemCart.Quantity);
            if (!ValidOperation()) return CustomResponse();
            var response = await _customerCartService.UpdateItemCart(productId, itemCart);
            return CustomResponse(response);
        }

        [HttpDelete]
        [Route("porchases/cart/items/{productId}")]
        public async Task<IActionResult> RemoveItemCart(Guid productId)
        {
            var product = _catalogService.GetById(productId);
            if (product == null)
            {
                AddProcessingErrors("Produto inexistente.");
                return CustomResponse();
            }
            var response = await _customerCartService.RemoveItemCart(productId);
            return CustomResponse(response);
        }

        [HttpPost]
        [Route("porchases/cart/apply-voucher")]
        public async Task<IActionResult> ApplyVoucher([FromBody] string voucherCode)
        {
            var voucher = await _orderService.GetVoucherByCode(voucherCode);
            if (voucher is null)
            {
                AddProcessingErrors("Voucher inválido ou não encontrado!");
                return CustomResponse();
            }
            var response = await _customerCartService.ApplyVoucherCart(voucher);
            return CustomResponse(response);
        }

        private async Task ValidItemCart(ItemProductDTO product, int quantity, bool addproduct = false)
        {
            if (product == null) AddProcessingErrors("product inexistente!");
            if (quantity < 1) AddProcessingErrors($"Escolha ao menos uma unidade do product {product.Name}");

            var cart = await _customerCartService.GetCustomerCart();
            var itemCart = cart.Items.FirstOrDefault(p => p.ProductId == product.ItemId);

            if (itemCart != null && addproduct && itemCart.Quantity + quantity > product.StockQuantity)
            {
                AddProcessingErrors($"O product {product.Name} possui {product.StockQuantity} unidades em estoque, você selecionou {quantity}");
                return;
            }

            if (quantity > product.StockQuantity) AddProcessingErrors($"O product {product.Name} possui {product.StockQuantity} unidades em estoque, você selecionou {quantity}");
        }
    }
}
