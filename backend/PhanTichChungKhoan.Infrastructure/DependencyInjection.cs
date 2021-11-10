using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PhanTichChungKhoan.Application.Interfaces;
using PhanTichChungKhoan.Infrastructure.DbContexts;
using PhanTichChungKhoan.Infrastructure.Repository;
using System;

namespace PhanTichChungKhoan.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<PriceBoardDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("PriceBoardConnection"));
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });
          
            //services.AddDbContext<AccountDbContext>(options =>
            //    options.UseSqlServer(configuration.GetConnectionString("AccountConnection")));

            services.AddScoped<IPriceBoardEfRepository, PriceBoardEfRepository>();
            services.AddScoped<IDapperRepository, DapperRepository>();

            return services;
        }
    }
}
