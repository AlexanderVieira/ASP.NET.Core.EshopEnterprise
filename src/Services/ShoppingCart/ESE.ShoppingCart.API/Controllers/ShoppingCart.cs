using ESE.ShoppingCart.API.Data;
using ESE.ShoppingCart.API.Models;
using ESE.WebAPI.Core.AspNetUser.Interfaces;
using ESE.WebAPI.Core.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ESE.ShoppingCart.API.Controllers
{
    public class ShoppingCartController : BaseController
    {
        private readonly IAspNetUser _user;
        private readonly ShoppingCartContext _context;

        public ShoppingCartController(IAspNetUser user, ShoppingCartContext context)
        {
            _user = user;
            _context = context;
        }

        [HttpGet("cart")]
        public async Task<CustomerCart> GetCart()
        {
            return await GetCustomerCart() ?? new CustomerCart();
        }

        [HttpPost("cart")]
        public async Task<IActionResult> AddItemCart(ItemCart item)
        {
            var cart = await GetCustomerCart();
            if (cart == null)
            {
                NewCartHandler(item);
            }
            else
            {
                ExistingCartHandler(cart, item);
            }

            if (!ValidOperation())
            {
                return CustomResponse();
            }

            await PersistData();
            return CustomResponse();
        }

        [HttpPut("cart/{productId}")]
        public async Task<IActionResult> UpdateItemCart(Guid productId, ItemCart item)
        {
            return CustomResponse();
        }

        private Task PersistData()
        {
            throw new NotImplementedException();
        }

        private void ExistingCartHandler(CustomerCart cart, ItemCart item)
        {
            throw new NotImplementedException();
        }

        private void NewCartHandler(ItemCart item)
        {
            throw new NotImplementedException();
        }

        private Task<CustomerCart> GetCustomerCart()
        {
            throw new NotImplementedException();
        }
    }
}
