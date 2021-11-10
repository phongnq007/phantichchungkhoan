using PhanTichChungKhoan.Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PhanTichChungKhoan.Application.Interfaces
{
    public interface IDapperRepository
    {
        Task<List<MyPriceBoardWithPriceDto>> ListMyPriceBoardAsync(string userId);
    }
}
