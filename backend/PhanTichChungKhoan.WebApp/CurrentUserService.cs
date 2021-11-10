using Microsoft.AspNetCore.Http;
using PhanTichChungKhoan.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PhanTichChungKhoan.WebApp
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly HttpContext _httpContext;

        public string Name { get; set; }
        public string UserId { get; set; }

        public CurrentUserService(IHttpContextAccessor contextAccessor)
        {
            var userPrincipal = contextAccessor.HttpContext?.User;

            Name = GetClaim(userPrincipal, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name") ?? string.Empty;
            UserId = GetClaim(userPrincipal, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier") ?? string.Empty;

            _httpContext = contextAccessor.HttpContext;
        }

        string GetClaim(ClaimsPrincipal claimPrincipals, string claimType)
        {
            return claimPrincipals?.Claims?.FirstOrDefault(p => p.Type == claimType)?.Value;
        }
    }
}
