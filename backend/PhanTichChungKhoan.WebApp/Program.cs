using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Web.Administration;
using PhanTichChungKhoan.Infrastructure.DbContexts;
using PhanTichChungKhoan.WebApp.Data;
using Serilog;

namespace PhanTichChungKhoan.WebApp
{
    public class Program
    {
        private static readonly IConfiguration configuration = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddEnvironmentVariables()
             .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true, reloadOnChange: false)
             .AddJsonFile($"appsettings.json", optional: false, reloadOnChange: true)
             .Build();

        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                //.Enrich.FromLogContext()
                .CreateLogger();
            try
            {
                Log.Information($"Starting web host at {DateTime.UtcNow.AddHours(7):dd/MM/yyyy HH:mm:ss.fff}");
                var host = CreateHostBuilder(args).Build();

                using (var scope = host.Services.CreateScope())
                {
                    //var accountDb = scope.ServiceProvider.GetRequiredService<AccountDbContext>();
                    var priceDb = scope.ServiceProvider.GetRequiredService<PriceBoardDbContext>();

                    //accountDb.Database.Migrate();
                    priceDb.Database.Migrate();
                    DbInitialize.SeedData(priceDb);
                }

                host.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        static void SetPoolIdleTimeout()
        {
            ServerManager serverManager = new ServerManager();
            ApplicationPoolCollection applicationPoolCollection = serverManager.ApplicationPools;
            foreach (var pool in applicationPoolCollection)
            {
                pool.ProcessModel.IdleTimeout = TimeSpan.Zero;
                //pool.Recycling.PeriodicRestart.Time = TimeSpan.FromMinutes(1440);
            }
            serverManager.CommitChanges();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureKestrel(serverOpt =>
                    {
                        serverOpt.Limits.KeepAliveTimeout = TimeSpan.FromHours(4);
                        serverOpt.Limits.RequestHeadersTimeout = TimeSpan.FromMinutes(2);
                    });
                    webBuilder.UseStartup<Startup>();
                });
    }
}
