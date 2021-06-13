using ESE.Store.MVC.Models;
using ESE.Store.MVC.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ESE.Store.MVC.Extensions
{
    public class CustomerCartViewComponent : ViewComponent
    {
        private readonly IPorchasesBffService _porchasesBffService;

        public CustomerCartViewComponent(IPorchasesBffService porchasesBffService)
        {
            _porchasesBffService = porchasesBffService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await _porchasesBffService.GetQuantityCustomerCart());
        }
    }
}
