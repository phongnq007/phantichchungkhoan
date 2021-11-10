using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PhanTichChungKhoan.WebApi;

namespace PhanTichChungKhoan.WebApi.IntegrationTests.Config
{
    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration)
            : base(configuration)
        {

        }

        protected override void ConfigureSwagger(IServiceCollection services)
        {
            //no need Swagger
        }

        protected override void UseAuth(IApplicationBuilder app)
        {
            //app.UseMiddleware<AuthenticationTestMiddleware>();
            //app.UseAuthorization();
        }

        protected override void UseSwagger(IApplicationBuilder app)
        {
            //no need Swagger
        }
    }
}
