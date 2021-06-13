using ESE.Core.Comunication;
using ESE.Porchase.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ESE.Porchase.API.Services.Interfaces
{
    public interface IOrderService
    {
        Task<ResponseResult> Checkout(OrderDTO order);
        Task<OrderDTO> GetLastOrder();
        Task<IEnumerable<OrderDTO>> GetListByClientId();
        Task<VoucherDTO> GetVoucherByCode(string code);
    }
}
