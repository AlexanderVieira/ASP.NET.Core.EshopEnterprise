using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ESE.Auth.API.Models;
using ESE.WebAPI.Core.Controllers;
using ESE.Core.Messages.Integration;
using ESE.MessageBus.Interfaces;
using ESE.Auth.API.Services;

namespace ESE.Auth.API.Controllers
{
    [Route("api/auth")]
    public class AuthController : BaseController
    {
        private readonly AuthenticationService _authenticationService;
        private readonly IMessageBus _bus;

        public AuthController(AuthenticationService authenticationService, IMessageBus bus)
        {
            _authenticationService = authenticationService;
            _bus = bus;
        }

        [HttpPost("new-account")]
        public async Task<ActionResult> Register(UserRegister userRegister){

            if (!ModelState.IsValid) return CustomResponse(ModelState);
            
            var user = new IdentityUser
            {
                UserName = userRegister.Email,
                Email = userRegister.Email,
                EmailConfirmed = true
            };

            var result = await _authenticationService.UserManager.CreateAsync(user, userRegister.Password);

            if (result.Succeeded)
            {
                var customerResult = await CustomerRecord(userRegister);
                if (!customerResult.ValidationResult.IsValid)
                {
                    await _authenticationService.UserManager.DeleteAsync(user);
                    return CustomResponse(customerResult.ValidationResult);
                }
                return CustomResponse(await _authenticationService.GenerateJwt(userRegister.Email));
            }

            foreach (var error in result.Errors)
            {
                AddProcessingErrors(error.Description);
            }

            return CustomResponse();
        }        

        [HttpPost("authenticate")]
        public async Task<ActionResult> Login(UserLogin userLogin){

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var result = await _authenticationService.SignInManager.PasswordSignInAsync(userLogin.Email, userLogin.Password, false, true);

            if (result.Succeeded)
            {
                return CustomResponse(await _authenticationService.GenerateJwt(userLogin.Email));
            }

            if (result.IsLockedOut)
            {
                AddProcessingErrors("Usuário temporariamente bloqueado por tentativas inválidas");
                return CustomResponse();
            }

            AddProcessingErrors("Usuário ou Senha incorretos");
            return CustomResponse();
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult> RefreshToken([FromBody] string refreshToken)
        {
            if (string.IsNullOrEmpty(refreshToken))
            {
                AddProcessingErrors("Refresh Token inválido");
                return CustomResponse();
            }

            var token = await _authenticationService.GetRefreshToken(Guid.Parse(refreshToken));

            if (token is null)
            {
                AddProcessingErrors("Refresh Token expirado");
                return CustomResponse();
            }

            return CustomResponse(await _authenticationService.GenerateJwt(token.Username));
        }

        private async Task<ResponseMessage> CustomerRecord(UserRegister userRegister)
        {
            var user = await _authenticationService.UserManager.FindByEmailAsync(userRegister.Email);
            var registeredUser = new RegisteredUserIntegrationEvent(Guid.Parse(user.Id), userRegister.Name, userRegister.Email, userRegister.Cpf);
            try
            {
                return await _bus.RequestAsync<RegisteredUserIntegrationEvent, ResponseMessage>(registeredUser);
            }
            catch
            {
                await _authenticationService.UserManager.DeleteAsync(user);
                throw;
            }
        }
    }
}