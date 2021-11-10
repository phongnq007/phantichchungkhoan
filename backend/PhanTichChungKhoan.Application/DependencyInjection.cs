using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PhanTichChungKhoan.Application.Configs;
using PhanTichChungKhoan.Application.Interfaces;

namespace PhanTichChungKhoan.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IMyPriceBoardUsecase, MyPriceBoardUsecase>();
            //services.AddScoped<IWebCrawler, VnDirectCrawler>();
            
            return services;
        }
    }
}
