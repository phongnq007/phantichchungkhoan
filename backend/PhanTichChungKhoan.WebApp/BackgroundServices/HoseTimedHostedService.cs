using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PhanTichChungKhoan.Application.Enums;
using PhanTichChungKhoan.Application.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PhanTichChungKhoan.WebApp.BackgroundServices
{
    public class HoseTimedHostedService : IHostedService, IDisposable
    {
        private int isWorking = 0;
        private readonly ILogger<HoseTimedHostedService> _logger;
        private Timer _timer;
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;

        public HoseTimedHostedService(ILogger<HoseTimedHostedService> logger, IServiceProvider serviceProvider, IConfiguration configuration)
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

                _logger.LogInformation($"{ExchangeSymbol.HOSE} background service running.");

                _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromMinutes(Convert.ToInt32(interval)));
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
                _logger.LogError(ex, $"{ExchangeSymbol.HOSE} - Ended DoWork with error.");
            }
        }

        async Task ExtractData()
        {
            _logger.LogInformation($"{ExchangeSymbol.HOSE} - Started crawling data at {DateTime.UtcNow.AddHours(7):dd/MM/yyyy HH:mm:ss.fff}");
            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var priceBoardEfRepository = scope.ServiceProvider.GetRequiredService<IPriceBoardEfRepository>();
                    var crawlerTool = scope.ServiceProvider.GetRequiredService<IWebCrawler>();

                    await priceBoardEfRepository.ExecuteSqlRawAsync(@"
                        DELETE FROM dbo.HosePriceBoardTemp; 
                        DELETE FROM dbo.HnxPriceBoardTemp;
                        DELETE FROM dbo.UpcomPriceBoardTemp;");

                    await crawlerTool.ExtractDataAsync(ExchangeSymbol.HOSE);

                    await priceBoardEfRepository.ExecuteSqlRawAsync($@"EXEC [dbo].[SyncPriceBoardDataHose]");
                }

                _logger.LogInformation($"{ExchangeSymbol.HOSE} - Ended crawling data.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{ExchangeSymbol.HOSE} - Ended crawling data with error.");
            }
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation($"{ExchangeSymbol.HOSE} background service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
