using PhanTichChungKhoan.Application.DTO;
using PhanTichChungKhoan.Application.Interfaces;
using PhanTichChungKhoan.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Ardalis.Specification;

namespace PhanTichChungKhoan.Application
{
    public class MyPriceBoardUsecase : IMyPriceBoardUsecase
    {
        private readonly IDapperRepository _dapperRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IPriceBoardEfRepository _priceBoardEfRepository;

        public MyPriceBoardUsecase(IDapperRepository dapperRepository
            , ICurrentUserService currentUserService, IPriceBoardEfRepository priceBoardEfRepository)
        {
            _dapperRepository = dapperRepository;
            _currentUserService = currentUserService;
            _priceBoardEfRepository = priceBoardEfRepository;
        }

        public async Task<List<MyPriceBoardWithPriceDto>> ListMyPriceBoard()
        {
            var data = await _dapperRepository.ListMyPriceBoardAsync(_currentUserService.UserId);
            return data;
        }

        public async Task<MyPriceBoardWithPriceDto> GetBuyingRangeByKey(string exchange, string symbol)
        {
            var entity = await _priceBoardEfRepository.FirstOrDefaultAsync(
                new GetMyPriceBoardByKeySpec(_currentUserService.UserId, exchange, symbol)
                );
            if (entity != null)
            {
                return MyPriceBoardWithPriceDto.FromMyPriceBoard(entity);
            }

            return null;
        }

        public async Task<bool> DeleteBuyingRange(string exchange, string symbol)
        {
            var entity = await _priceBoardEfRepository.FirstOrDefaultAsync(
                new GetMyPriceBoardByKeySpec(_currentUserService.UserId, exchange, symbol)
                );
            if (entity != null)
            {
                _priceBoardEfRepository.DeleteRange<MyPriceBoard>(new MyPriceBoard[] { entity });
                await _priceBoardEfRepository.SaveChangesAsync();
            }

            return true;
        }

        public async Task<bool> UpdateBuyingRange(MyPriceBoardWithPriceDto myPriceBoard)
        {
            var entity = await _priceBoardEfRepository.FirstOrDefaultAsync(
                new GetMyPriceBoardByKeySpec(_currentUserService.UserId, myPriceBoard.Exchange, myPriceBoard.Symbol)
                );
            if (entity != null)
            {
                entity.BuyPriceFrom = myPriceBoard.BuyPriceFrom;
                entity.BuyPriceTo = myPriceBoard.BuyPriceTo;
                
                _priceBoardEfRepository.UpdateRange<MyPriceBoard>(new MyPriceBoard[] { entity });
                await _priceBoardEfRepository.SaveChangesAsync();
            }

            return true;
        }

        public async Task<bool> AddBuyingRange(MyPriceBoardWithPriceDto buyingRangeDto)
        {
            var mypriceboard = MyPriceBoardWithPriceDto.ToMyPriceBoard(buyingRangeDto);

            var priceBoard = await _priceBoardEfRepository.FirstOrDefaultAsync<PriceBoard>(
                new GetExchangeBySymbolSpecification(mypriceboard.Symbol)
                );

            if (priceBoard is null)
            {
                throw new ArgumentNullException($"Could not find the price of this symbol {mypriceboard.Symbol}.");
            }

            mypriceboard.Exchange = priceBoard.Exchange;
            mypriceboard.UserId = _currentUserService.UserId;
            mypriceboard.UpdatedDate = DateTimeOffset.UtcNow;

            await _priceBoardEfRepository.AddAsync<MyPriceBoard>(mypriceboard);
            await _priceBoardEfRepository.SaveChangesAsync();
            return true;
        }
    }

    public class GetExchangeBySymbolSpecification : Specification<PriceBoard>
    {
        public GetExchangeBySymbolSpecification(string symbol)
        {
            Query.Where(x => x.Symbol == symbol);
        }
    }

    public class GetMyPriceBoardByKeySpec : Specification<MyPriceBoard>
    {
        public GetMyPriceBoardByKeySpec(string userId, string exchange, string symbol)
        {
            Query.Where(x => x.Symbol == symbol && x.Exchange == exchange && x.UserId == userId);
        }
    }
}
