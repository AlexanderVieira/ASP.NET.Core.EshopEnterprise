using ESE.Store.MVC.Models;
using ESE.Store.MVC.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ESE.Store.MVC.Extensions
{
    public class CustomerCartViewComponent : ViewComponent
    {
        private readonly ICustomerCartService _cartService;

        public CustomerCartViewComponent(ICustomerCartService cartService)
        {
            _cartService = cartService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await _cartService.GetCustomerCart() ?? new CustomerCartViewModel());
        }
    }
}
