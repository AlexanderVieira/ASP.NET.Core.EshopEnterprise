using ESE.Store.MVC.Models;
using ESE.Store.MVC.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace ESE.Store.MVC.Controllers
{
    [Authorize]
    public class ClientController : BaseController
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpPost]
        public async Task<IActionResult> NewAddress(AddressViewModel address)
        {
            var response = await _clientService.AddAddress(address);
            if (HasResponseErrors(response))
            {
                TempData["Erros"] = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList();
            }
            return RedirectToAction("DeliveryAddress", "Order");
        }
    }
}
