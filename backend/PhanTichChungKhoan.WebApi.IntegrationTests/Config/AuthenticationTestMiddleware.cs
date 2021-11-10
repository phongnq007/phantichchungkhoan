using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PhanTichChungKhoan.WebApi.IntegrationTests.Config
{
    public class AuthenticationTestMiddleware
    {
        const string Identity_ID = "44A2D2C8-D4F7-4E43-B255-1340FD7F3482";
        const string Identity_Name = "Integration test user";

        private readonly RequestDelegate _next;

        public AuthenticationTestMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            var identity = new ClaimsIdentity("cookies");

            identity.AddClaim(new Claim("sub", Identity_ID));
            identity.AddClaim(new Claim("unique_name", Identity_ID));
            identity.AddClaim(new Claim("name", Identity_Name));
            //add more claim if need

            httpContext.User.AddIdentity(identity);
            await _next.Invoke(httpContext);
        }
    }
}
