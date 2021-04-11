using System.Threading.Tasks;
using ESE.WebAPI.Core.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESE.Porchase.API.Controllers
{
    [Authorize]
    public class ShoppingCartController : BaseController
    {
        [HttpGet]
        [Route("porchases/cart")]
        public async Task<IActionResult> Index()
        {
            return CustomResponse();
        }

        [HttpGet]
        [Route("porchases/cart-quantity")]
        public async Task<IActionResult> GetCartQuantity()
        {
            return CustomResponse();
        }

        [HttpPost]
        [Route("porchases/cart/items")]
        public async Task<IActionResult> AddItemCart()
        {
            return CustomResponse();
        }

        [HttpPut]
        [Route("porchases/cart/items/{productId}")]
        public async Task<IActionResult> UpdateItemCart()
        {
            return CustomResponse();
        }

        [HttpDelete]
        [Route("porchases/cart/items/{productId}")]
        public async Task<IActionResult> RemoveItemCart()
        {
            return CustomResponse();
        }
    }
}
