using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PhanTichChungKhoan.Infrastructure.DbContexts;

namespace PhanTichChungKhoan.WebApi.IntegrationTests.Config
{
    public abstract class InMemoryTestBase
    {
        protected PriceBoardDbContext _priceBoardDbContext;

        private static IConfiguration _configuration { get; } = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile($"appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();

        private IServiceProvider _serviceProvider;

        public object Resolve(Type t)
        {
            if (_serviceProvider is null)
            {
                return null;
            }
            return _serviceProvider.GetRequiredService(t);
        }

        protected async Task<HttpClient> CreateClientAsync()
        {
            //_configuration["SharedFolder"] = $"{AppDomain.CurrentDomain.BaseDirectory}\\Resources";

            var hostBuilder = new HostBuilder()
                .ConfigureWebHost(webHost => 
                {
                    webHost.UseTestServer()
                       .UseConfiguration(_configuration)
                       .UseStartup<TestStartup>();
                })
                .ConfigureServices(services => ConfigureServices(services));
            var host = await hostBuilder.StartAsync();

            var client = host.GetTestClient();
            client.Timeout = TimeSpan.FromMinutes(5);

            return client;
        }

        void ConfigureServices(IServiceCollection services)
        {
            //services.AddHttpClient();
            ConfigurePriceBoardDb(services);
            //configure more service if need mock
            _serviceProvider = services.BuildServiceProvider();
        }

        void ConfigurePriceBoardDb(IServiceCollection services)
        {
            var dbName = _configuration.GetConnectionString("PriceBoardConnection");
            
            var descriptor = services.SingleOrDefault(x => x.ServiceType == typeof(DbContextOptions<PriceBoardDbContext>));
            services.Remove(descriptor);

            services.AddDbContext<PriceBoardDbContext>(options =>
            {
                options.UseInMemoryDatabase(dbName);
            });

            var sp = services.BuildServiceProvider();
            _priceBoardDbContext = sp.GetRequiredService<PriceBoardDbContext>();
            _priceBoardDbContext.Database.EnsureDeleted();
            _priceBoardDbContext.Database.EnsureCreated();
        }
    }
}
