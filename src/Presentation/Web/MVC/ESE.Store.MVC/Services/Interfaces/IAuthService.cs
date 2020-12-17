using ESE.Store.MVC.Models;
using System.Threading.Tasks;

namespace ESE.Store.MVC.Services.Interfaces
{
    public interface IAuthService
    {
        Task<UserResponseLogin> Login(UserLogin userLogin);
        Task<UserResponseLogin> Register(UserRegister userRegister);
    }
}
