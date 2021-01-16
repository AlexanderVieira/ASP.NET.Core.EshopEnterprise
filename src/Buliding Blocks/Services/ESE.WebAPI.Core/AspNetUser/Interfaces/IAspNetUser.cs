using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace ESE.WebAPI.Core.AspNetUser.Interfaces
{
    public interface IAspNetUser
    {
        public string Name { get; }
        Guid GetUserId();
        string GetUserEmail();
        string GetUserToken();
        bool IsAuthenticated();
        bool HasRole(string role);
        IEnumerable<Claim> GetClaims();
        HttpContext GetHttpContext();
    }
}
