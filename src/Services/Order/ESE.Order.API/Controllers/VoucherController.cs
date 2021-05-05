using ESE.Order.API.Application.DTO;
using ESE.Order.API.Application.Interfaces;
using ESE.WebAPI.Core.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace ESE.Order.API.Controllers
{
    public class VoucherController : BaseController
    {
        private readonly IVoucherQueries _voucherQueries;

        public VoucherController(IVoucherQueries voucherQueries)
        {
            _voucherQueries = voucherQueries;
        }

        [HttpGet("voucher/{code}")]
        [ProducesResponseType(typeof(VoucherDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetVoucherByCode(string code)
        {
            if (string.IsNullOrEmpty(code)) return NotFound();
            var voucher = await _voucherQueries.GetVoucherByCode(code);
            return voucher == null ? NotFound() : CustomResponse(voucher);
        }
    }
}
