using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PhanTichChungKhoan.Application;
using PhanTichChungKhoan.Application.Configs;
using PhanTichChungKhoan.Application.Enums;
using PhanTichChungKhoan.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PhanTichChungKhoan.WebApp.BackgroundServices
{
    public class VnDirectTimedHostedService : IHostedService, IDisposable
    {
        private int isWorking = 0;
        private readonly ILogger<VnDirectTimedHostedService> _logger;
        private Timer _timer;
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;

        public VnDirectTimedHostedService(ILogger<VnDirectTimedHostedService> logger, IServiceProvider serviceProvider, IConfiguration configuration)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _configuration = configuration;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            var interval = _configuration.GetSection("BackgroundService:VnDirectInterval").Value;
            interval = string.IsNullOrWhiteSpace(interval) ? "1" : interval;

            _logger.LogInformation("VnDirect background service running.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromMinutes(Convert.ToInt32(interval)));

            return Task.CompletedTask;
        }

        private void DoWorkTest(object state)
        {
            try
            {
                if (1 == Interlocked.Exchange(ref isWorking, 1))
                {
                    return;
                }

                var currentTime = DateTime.UtcNow.AddHours(7);
                _logger.LogInformation($"Started crawling data at {currentTime.ToString("dd/MM/yyyy HH:mm:ss")}");

                Thread.Sleep(90 * 1000);

                _logger.LogInformation("Ended crawling data.");
                Interlocked.Exchange(ref isWorking, 0);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ended DoWorkTest with error.");
            }
        }

        private void DoWork(object state)
        {
            try
            {
                var currentTime = DateTime.UtcNow.AddHours(7);
                if (0 < currentTime.Hour && currentTime.Hour < 9)
                {
                    return; //out of VN trading time
                }

                if (1 == Interlocked.Exchange(ref isWorking, 1))
                {
                    return;
                }

                ExtractData().Wait();

                Interlocked.Exchange(ref isWorking, 0);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ended DoWork with error.");
            }
        }

        async Task ExtractData()
        {
            _logger.LogInformation("Started crawling data.");
            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var securityEndpointOption = scope.ServiceProvider.GetRequiredService<IOptions<SecurityEndpointOption>>();
                    var priceBoardEfRepository = scope.ServiceProvider.GetRequiredService<IPriceBoardEfRepository>();
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<VnDirectCrawler>>();

                    //await priceBoardEfRepository.ExecuteSqlRawAsync("DELETE FROM dbo.PriceBoardTemp");

                    //using (var crawlerTool = new VnDirectCrawler(securityEndpointOption, priceBoardEfRepository, logger))
                    //{
                    //    await crawlerTool.ExtractDataAsync(ExchangeSymbol.HOSE);
                    //    await crawlerTool.ExtractDataAsync(ExchangeSymbol.HNX);
                    //}

                    //await priceBoardEfRepository.ExecuteSqlRawAsync(@"EXEC [dbo].[SyncPriceBoardData]");
                }

                _logger.LogInformation("Ended crawling data.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ended crawling data with error.");
            }
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("VnDirect background service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
