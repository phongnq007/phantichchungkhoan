using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using PhanTichChungKhoan.Application.Configs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhanTichChungKhoan.WebApi
{
    public static class ConfigureProblemDetailExtension
    {
        private const string ProblemDetailMessageKey = "phong-handlexception-message";

        public static void ConfigureProblemDetail(this IServiceCollection services)
        {
            services.AddProblemDetails(opt =>
            {
                opt.OnBeforeWriteDetails = (ctx, problem) =>
                {
                    if (ctx.Items.ContainsKey(ProblemDetailMessageKey))
                    {
                        problem.Detail = (string)ctx.Items[ProblemDetailMessageKey];
                    }
                };

                opt.IncludeExceptionDetails = (ctx, ex) =>
                {
                    if (!ctx.Items.ContainsKey(ProblemDetailMessageKey))
                    {
                        ctx.Items.Add(ProblemDetailMessageKey, ex.Message);
                    }
                    return false;
                };

                opt.MapToStatusCode<BusinessExceptionStatus500>(StatusCodes.Status500InternalServerError);
            });
        }
    }
}
