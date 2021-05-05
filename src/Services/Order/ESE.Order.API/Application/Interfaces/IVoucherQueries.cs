using ESE.Order.API.Application.DTO;
using System.Threading.Tasks;

namespace ESE.Order.API.Application.Interfaces
{
    public interface IVoucherQueries
    {
        Task<VoucherDTO> GetVoucherByCode(string code);
    }
}
