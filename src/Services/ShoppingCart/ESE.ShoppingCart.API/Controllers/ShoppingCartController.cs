using ESE.ShoppingCart.API.Data;
using ESE.ShoppingCart.API.Models;
using ESE.WebAPI.Core.AspNetUser.Interfaces;
using ESE.WebAPI.Core.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ESE.ShoppingCart.API.Controllers
{
    [Authorize]
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
            var cart = await GetCustomerCart();
            var itemCart = await GetItemCartValid(productId, cart, item);
            if (itemCart == null)
            {
                return CustomResponse();
            }

            cart.UpdateUnits(itemCart, item.Quantity);

            ValidCart(cart);

            if (!ValidOperation())
            {
                return CustomResponse();
            }

            _context.ItemCarts.Update(itemCart);
            _context.CustomerCarts.Update(cart);

            await PersistData();
            return CustomResponse();
        }

        [HttpDelete("cart/{productId}")]
        public async Task<IActionResult> RemoveItemCart(Guid productId)
        {
            var cart = await GetCustomerCart();
            var itemCart = await GetItemCartValid(productId, cart);
            if (itemCart == null)
            {
                return CustomResponse();
            }

            cart.RemoveItem(itemCart);

            _context.ItemCarts.Remove(itemCart);
            _context.CustomerCarts.Update(cart);

            await PersistData();
            return CustomResponse();
        }

        [HttpPost]
        [Route("cart/apply-voucher")]
        public async Task<IActionResult> ApplyVoucher(Voucher voucher)
        {
            var cart = await GetCustomerCart();

            cart.ApplyVoucher(voucher);

            _context.CustomerCarts.Update(cart);

            await PersistData();
            return CustomResponse();
        }

        private async Task PersistData()
        {
            var result = await _context.SaveChangesAsync();
            if (result <= 0)
            {
                AddProcessingErrors("Não foi possível persistir os dados no banco.");
            }
        }

        private void ExistingCartHandler(CustomerCart cart, ItemCart item)
        {
            var existingProductItem = cart.ExistingItemCart(item);

            cart.AddItem(item);
            ValidCart(cart);

            if (existingProductItem)
            {
                _context.ItemCarts.Update(cart.GetByProductId(item.ProductId));
            }
            else
            {
                _context.ItemCarts.Add(item);
            }

            _context.CustomerCarts.Update(cart);
        }

        private void NewCartHandler(ItemCart item)
        {
            var cart = new CustomerCart(_user.GetUserId());
            cart.AddItem(item);
            _context.CustomerCarts.Add(cart);
        }

        private async Task<CustomerCart> GetCustomerCart()
        {
            return await _context.CustomerCarts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.CustomerId == _user.GetUserId());
        }

        private async Task<ItemCart> GetItemCartValid(Guid productId, CustomerCart cart, ItemCart item = null)
        {
            if (item != null && productId != item.ProductId)
            {
                AddProcessingErrors("O item não corresponde ao informado.");
                return null;
            }

            if (cart == null)
            {
                AddProcessingErrors("Carrinho não encontrado.");
                return null;
            }

            var itemCart = await _context.ItemCarts
                .FirstOrDefaultAsync(i => i.CustomerCartId == cart.Id && i.ProductId == productId);

            if (itemCart == null || !cart.ExistingItemCart(itemCart))
            {
                AddProcessingErrors("O item não está no carrinho.");
                return null;
            }

            return itemCart;
        }

        private bool ValidCart(CustomerCart cart)
        {
            if (cart.IsValid())
            {
                return true;
            }

            cart.ValidationResult.Errors.ToList().ForEach(e => AddProcessingErrors(e.ErrorMessage));
            return false;
        }
    }
}
