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
    public class HnxTimedHostedService : IHostedService, IDisposable
    {
        private int isWorking = 0;
        private readonly ILogger<HnxTimedHostedService> _logger;
        private Timer _timer;
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;

        public HnxTimedHostedService(ILogger<HnxTimedHostedService> logger, IServiceProvider serviceProvider, IConfiguration configuration)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _configuration = configuration;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            var disabled = _configuration.GetSection("BackgroundService:Disabled").Value.Trim();
            if (!disabled.Equals("1"))
            {
                var interval = _configuration.GetSection("BackgroundService:VnDirectInterval").Value;
                interval = string.IsNullOrWhiteSpace(interval) ? "1" : interval;

                _logger.LogInformation($"{ExchangeSymbol.HNX} background service running.");

                _timer = new Timer(DoWork, null, TimeSpan.FromSeconds(5), TimeSpan.FromMinutes(Convert.ToInt32(interval)));
            }
            return Task.CompletedTask;
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
                _logger.LogError(ex, $"{ExchangeSymbol.HNX} - Ended DoWork with error.");
            }
        }

        async Task ExtractData()
        {
            _logger.LogInformation($"{ExchangeSymbol.HNX} - Started crawling data.");
            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var priceBoardEfRepository = scope.ServiceProvider.GetRequiredService<IPriceBoardEfRepository>();
                    var crawlerTool = scope.ServiceProvider.GetRequiredService<IWebCrawler>();

                    await priceBoardEfRepository.ExecuteSqlRawAsync("DELETE FROM dbo.HnxPriceBoardTemp");

                    await crawlerTool.ExtractDataAsync(ExchangeSymbol.HNX);

                    await priceBoardEfRepository.ExecuteSqlRawAsync($@"EXEC [dbo].[SyncPriceBoardData] '{ExchangeSymbol.HNX}'");
                }

                _logger.LogInformation($"{ExchangeSymbol.HNX} - Ended crawling data.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{ExchangeSymbol.HNX} - Ended crawling data with error.");
            }
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation($"{ExchangeSymbol.HNX} background service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
