using PhanTichChungKhoan.Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PhanTichChungKhoan.Application.Interfaces
{
    public interface IMyPriceBoardUsecase
    {
        Task<List<MyPriceBoardWithPriceDto>> ListMyPriceBoard();
        Task<bool> AddBuyingRange(MyPriceBoardWithPriceDto buyingRangeDto);
        Task<bool> DeleteBuyingRange(string exchange, string symbol);
        Task<MyPriceBoardWithPriceDto> GetBuyingRangeByKey(string exchange, string symbol);
        Task<bool> UpdateBuyingRange(MyPriceBoardWithPriceDto myPriceBoard);

    }
}
