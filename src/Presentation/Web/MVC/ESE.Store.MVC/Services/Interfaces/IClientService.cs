using ESE.Store.MVC.Models;
using System.Threading.Tasks;

namespace ESE.Store.MVC.Services.Interfaces
{
    public interface IClientService
    {
        Task<AddressViewModel> GetAddress();
        Task<ResponseResult> AddAddress(AddressViewModel address);
    }
}
