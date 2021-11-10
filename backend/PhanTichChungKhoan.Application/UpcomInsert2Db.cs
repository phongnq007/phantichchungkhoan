using Microsoft.Extensions.Logging;
using PhanTichChungKhoan.Application.Enums;
using PhanTichChungKhoan.Application.Interfaces;
using PhanTichChungKhoan.Application.WebDrivers;
using PhanTichChungKhoan.Domain;
using System;
using System.Threading.Tasks;

namespace PhanTichChungKhoan.Application
{
    public class UpcomInsert2Db : IDisposable
    {
        private SeleniumWebDriver _seleniumWebDriver;
        private bool disposedValue;
        private readonly ILogger<VnDirectCrawler> _logger;
        private readonly string _endpoint;
        private readonly IPriceBoardEfRepository _priceBoardEfRepository;

        public UpcomInsert2Db(IPriceBoardEfRepository priceBoardEfRepository, ILogger<VnDirectCrawler> logger, string endpoint)
        {
            _seleniumWebDriver = new EdgeWebDriver(logger);
            _priceBoardEfRepository = priceBoardEfRepository;
            _logger = logger;
            _endpoint = endpoint;
        }

        public async Task ExecuteAsync()
        {
            _logger.LogInformation($"Start inserting price data for {ExchangeSymbol.UPCOM}.");

            await _seleniumWebDriver.GetListPriceBoard(_endpoint, ExchangeSymbol.UPCOM);

            if (_seleniumWebDriver.ListPriceBoard.Count > 0)
            {
                _logger.LogInformation($"Number of symbols inserted to Db for {ExchangeSymbol.UPCOM} : {_seleniumWebDriver.ListPriceBoard.Count}.");

                await _priceBoardEfRepository.AddRangeAsync<UpcomPriceBoardTemp>(
                    UpcomPriceBoardTemp.FromPriceBoardTemp(_seleniumWebDriver.ListPriceBoard)
                    );
                await _priceBoardEfRepository.SaveChangesAsync();
            }

            _logger.LogInformation($"End inserting price data for {ExchangeSymbol.UPCOM}.");
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _seleniumWebDriver.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
