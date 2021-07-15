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
        private readonly IPorchasesBffService _porchasesBffService;        

        public ShoppingCartController(IPorchasesBffService porchasesBffService)
        {
            _porchasesBffService = porchasesBffService;            
        }

        [HttpGet]
        [Route("cart")]
        public async Task<IActionResult> Index()
        {
            return View(await _porchasesBffService.GetCustomerCart());
        }

        [HttpPost]
        [Route("cart/add-item")]
        public async Task<IActionResult> AddItemCart(ItemCartViewModel item) 
        {
            var response = await _porchasesBffService.AddItemCart(item);
            if (HasResponseErrors(response))
            {
                return View("Index", await _porchasesBffService.GetCustomerCart());
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("cart/update-item")]
        public async Task<IActionResult> UpdateItemCart(Guid productId, int quantity)
        {            
            var item = new ItemCartViewModel { ProductId = productId, Quantity = quantity };
            var response = await _porchasesBffService.UpdateItemCart(productId, item);
            if (HasResponseErrors(response))
            {
                return View("Index", await _porchasesBffService.GetCustomerCart());
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("cart/remove-item")]
        public async Task<IActionResult> RemoveItemCart(Guid productId)
        {           
            var response = await _porchasesBffService.RemoveItemCart(productId);
            if (HasResponseErrors(response))
            {
                return View("Index", await _porchasesBffService.GetCustomerCart());
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("cart/apply-voucher")]
        public async Task<IActionResult> ApplyVoucher(string voucherCode)
        {
            var response = await _porchasesBffService.ApplyVoucherCustomerCart(voucherCode);
            if (HasResponseErrors(response))
            {
                return View("Index", await _porchasesBffService.GetCustomerCart());
            }
            return RedirectToAction("Index");
        }

    }
}
