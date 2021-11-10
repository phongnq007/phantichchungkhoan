using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PhanTichChungKhoan.Application.Configs;
using PhanTichChungKhoan.Application.Enums;
using PhanTichChungKhoan.Application.Interfaces;
using System.Threading.Tasks;

namespace PhanTichChungKhoan.Application
{
    public class VnDirectCrawler : IWebCrawler
    {
        private readonly SecurityEndpointOption _securityEndpointOption;
        private readonly IPriceBoardEfRepository _priceBoardEfRepository;
        private readonly ILogger<VnDirectCrawler> _logger;

        public VnDirectCrawler(IOptions<SecurityEndpointOption> configuration
            , IPriceBoardEfRepository priceBoardEfRepository
            , ILogger<VnDirectCrawler> logger)
        {
            _securityEndpointOption = configuration.Value;
            _priceBoardEfRepository = priceBoardEfRepository;
            _logger = logger;
        }

        public async Task ExtractDataAsync(ExchangeSymbol exchange)
        {
            switch (exchange)
            {
                case ExchangeSymbol.HOSE:
                    using (var obj = new HoseInsert2Db(_priceBoardEfRepository, _logger, _securityEndpointOption.VnDirect.Hose))
                    {
                        await obj.ExecuteAsync();
                        await obj.ExecuteForHnxAsync(_securityEndpointOption.VnDirect.Hnx);
                        await obj.ExecuteForUpcomAsync(_securityEndpointOption.VnDirect.Upcom);
                    }
                    
                    break;
                //case ExchangeSymbol.HNX:
                    //using (var obj = new HnxInsert2Db(_priceBoardEfRepository, _logger, _securityEndpointOption.VnDirect.Hnx))
                    //{
                    //    await obj.ExecuteAsync();
                    //}
                    //break;
                //case ExchangeSymbol.UPCOM:
                //    using (var obj = new UpcomInsert2Db(_priceBoardEfRepository, _logger, _securityEndpointOption.VnDirect.Upcom))
                //    {
                //        await obj.ExecuteAsync();
                //    }
                //    break;
            }
        }

    }
}
