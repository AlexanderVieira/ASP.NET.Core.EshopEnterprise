using ESE.Store.MVC.Models;
using System.Threading.Tasks;

namespace ESE.Store.MVC.Services.Interfaces
{
    public interface IAuthService
    {
        Task<UserResponseLogin> Login(UserLogin userLogin);
        Task<UserResponseLogin> Register(UserRegister userRegister);
        Task RealizarLogin(UserResponseLogin response);
        Task Logout();
        bool TokenExpirado();
        Task<bool> RefreshTokenValido();
    }
}
