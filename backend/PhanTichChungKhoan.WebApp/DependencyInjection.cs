using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PhanTichChungKhoan.Application.Interfaces;
using PhanTichChungKhoan.Infrastructure.DbContexts;
using PhanTichChungKhoan.Infrastructure.Repository;
using PhanTichChungKhoan.WebApp.BackgroundServices;
using PhanTichChungKhoan.WebApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhanTichChungKhoan.WebApp
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWebServices(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddDbContext<AccountDbContext>(options =>
            //    options.UseSqlServer(configuration.GetConnectionString("AccountConnection")));

            services.AddDefaultIdentity<IdentityUser>().AddEntityFrameworkStores<PriceBoardDbContext>();
            
            services.Configure<IdentityOptions>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;

                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
            });

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromHours(3);

                options.LoginPath = "/Identity/Account/Login";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.SlidingExpiration = true;
            });

            services.AddScoped<ICurrentUserService, CurrentUserService>();
            //services.AddHostedService<VnDirectTimedHostedService>();
            services.AddHostedService<HoseTimedHostedService>();
            //services.AddHostedService<HnxTimedHostedService>();
            //services.AddHostedService<UpcomTimedHostedService>();

            return services;
        }
    }
}
