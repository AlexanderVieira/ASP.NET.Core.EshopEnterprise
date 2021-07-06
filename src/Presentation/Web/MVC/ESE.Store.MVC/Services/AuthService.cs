using ESE.Store.MVC.Extensions;
using ESE.Store.MVC.Models;
using ESE.Store.MVC.Services.Interfaces;
using ESE.WebAPI.Core.AspNetUser.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ESE.Store.MVC.Services
{
    public class AuthService : TextSerializerService, IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly IAspNetUser _user;
        private readonly IAuthenticationService _authService;

        public AuthService(HttpClient httpClient, IAspNetUser user, IAuthenticationService authService, IOptions<AppSettings> settings)
        {
            httpClient.BaseAddress = new Uri(settings.Value.AuthUrl);
            _httpClient = httpClient;
            _user = user;
            _authService = authService;
        }        
        
        public async Task<UserResponseLogin> Login(UserLogin userLogin)
        {
            var loginContent = GetContent(userLogin);

            var response = await _httpClient.PostAsync("/api/auth/authenticate", loginContent);

            if (!HandlerResponseErrors(response))
            {
                return new UserResponseLogin
                {
                    ResponseResult = await DeserializeResponseObject<ResponseResult>(response)
                };
            }

            return await DeserializeResponseObject<UserResponseLogin>(response);
        }

        public async Task<UserResponseLogin> Register(UserRegister userRegister)
        {
            var registerContent = GetContent(userRegister);

            var response = await _httpClient.PostAsync("/api/auth/new-account", registerContent);

            if (!HandlerResponseErrors(response))
            {
                return new UserResponseLogin
                {
                    ResponseResult = await DeserializeResponseObject<ResponseResult>(response)
                };
            }

            return await DeserializeResponseObject<UserResponseLogin>(response);
        }

        public async Task<UserResponseLogin> UtilizarRefreshToken(string refreshToken)
        {
            var refreshTokenContent = GetContent(refreshToken);

            var response = await _httpClient.PostAsync("/api/auth/refresh-token", refreshTokenContent);

            if (!HandlerResponseErrors(response))
            {
                return new UserResponseLogin
                {
                    ResponseResult = await DeserializeResponseObject<ResponseResult>(response)
                };
            }

            return await DeserializeResponseObject<UserResponseLogin>(response);
        }

        public async Task Logout()
        {
            await _authService.SignOutAsync(
                _user.GetHttpContext(),
                CookieAuthenticationDefaults.AuthenticationScheme,
                null);
        }

        public async Task RealizarLogin(UserResponseLogin response)
        {
            var token = GetTokenFormated(response.AccessToken);

            var claims = new List<Claim>();
            claims.Add(new Claim("JWT", response.AccessToken));
            claims.Add(new Claim("RefreshToken", response.RefreshToken));
            claims.AddRange(token.Claims);

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(8),
                IsPersistent = true
            };

            await _authService.SignInAsync(_user.GetHttpContext(),
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }

        public async Task<bool> RefreshTokenValido()
        {
            var resposta = await UtilizarRefreshToken(_user.GetUserRefreshToken());

            if (resposta.AccessToken != null && resposta.ResponseResult == null)
            {
                await RealizarLogin(resposta);
                return true;
            }

            return false;
        }        

        public static JwtSecurityToken GetTokenFormated(string jwtToken)
        {
            return new JwtSecurityTokenHandler().ReadToken(jwtToken) as JwtSecurityToken;
        }
        
        public bool TokenExpirado()
        {
            var jwt = _user.GetUserToken();
            if (jwt is null) return false;

            var token = GetTokenFormated(jwt);
            return token.ValidTo.ToLocalTime() < DateTime.Now;
        }        
    }
}
