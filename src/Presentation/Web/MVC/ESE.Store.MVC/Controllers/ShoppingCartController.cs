using ESE.Store.MVC.Models;
using ESE.Store.MVC.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ESE.Store.MVC.Controllers
{ 
    public class ShoppingCartController : BaseController
    {
        private readonly ICustomerCartService _cartService;
        private readonly ICatalogService _catalogService;

        public ShoppingCartController(ICustomerCartService cartService, ICatalogService catalogService)
        {
            _cartService = cartService;
            _catalogService = catalogService;
        }

        [HttpGet]
        [Route("cart")]
        public async Task<IActionResult> Index()
        {
            return View(await _cartService.GetCustomerCart());
        }

        [HttpPost]
        [Route("cart/add-item")]
        public async Task<IActionResult> AddItemCart(ItemCartViewModel item) 
        {
            var product = await _catalogService.GetById(item.ProductId);
            ItemCartValid(product, item.Quantity);
            if (!ValidOperation())
            {
                return View("Index", await _cartService.GetCustomerCart());
            }

            item.Name = product.Name;
            item.Value = product.Value;
            item.Image = product.Image;

            var response = await _cartService.AddItemCart(item);

            if (HasResponseErrors(response))
            {
                return View("Index", await _cartService.GetCustomerCart());
            }

            return RedirectToAction("Index");
        }

        [HttpPut]
        [Route("cart/update-item")]
        public async Task<IActionResult> UpdateItemCart(Guid productId, int quantity)
        {
            var product = await _catalogService.GetById(productId);
            ItemCartValid(product, quantity);
            if (!ValidOperation())
            {
                return View("Index", await _cartService.GetCustomerCart());
            }

            var item = new ItemCartViewModel { ProductId = productId, Quantity = quantity };
            var response = await _cartService.UpdateItemCart(productId, item);

            if (HasResponseErrors(response))
            {
                return View("Index", await _cartService.GetCustomerCart());
            }

            return RedirectToAction("Index");
        }

        [HttpDelete]
        [Route("cart/remove-item")]
        public async Task<IActionResult> RemoveItemCart(Guid productId)
        {
            var product = await _catalogService.GetById(productId);
            if (product == null)
            {
                AddValidationError("Produto inexistente.");
                return View("Index", await _cartService.GetCustomerCart());
            }

            var response = await _cartService.RemoveItemCart(productId);
            if (HasResponseErrors(response))
            {
                return View("Index", await _cartService.GetCustomerCart());
            }

            return RedirectToAction("Index");
        }

        private void ItemCartValid(ProductViewModel product, int quantity)
        {
            if (product == null) AddValidationError("Produto inexistente.");
            if (quantity < 1) AddValidationError($"Escolha ao menos uma unidade do produto {product.Name}.");
            if (quantity > product.StockQuantity) AddValidationError($"O produto {product.Name} possui {product.StockQuantity} unidades em estoque.");            
        }
    }
}
