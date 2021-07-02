using ESE.Porchase.API.Models;
using System.Threading.Tasks;

namespace ESE.Porchase.API.Services.Interfaces
{
    public interface IClientService
    {
        Task<AddressDTO> GetAddress();
    }
}
