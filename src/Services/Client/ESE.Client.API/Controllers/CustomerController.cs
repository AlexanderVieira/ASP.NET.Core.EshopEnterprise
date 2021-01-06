using ESE.Client.API.Application.Commands;
using ESE.Core.Mediator.Interfaces;
using ESE.WebAPI.Core.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ESE.Client.API.Controllers
{
    public class CustomerController : BaseController
    {
        private readonly IMediatorHandler _mediatorHandler;

        public CustomerController(IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        [HttpGet("customers")]
        public async Task<IActionResult> Index()
        {
            var result = await _mediatorHandler.SendCommand(
                new RegisterCustomerCommand(Guid.NewGuid(), "Alexander Silva", "alexander.silva@teste.com", "09600768080"));
            return CustomResponse(result);
        }
    }
}
