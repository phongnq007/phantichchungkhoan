using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using PhanTichChungKhoan.Application.DTO;
using PhanTichChungKhoan.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanTichChungKhoan.Infrastructure.Repository
{
    public class DapperRepository : IDapperRepository
    {
        private readonly string _priceBoardConnection;

        public DapperRepository(IConfiguration configuration)
        {
            _priceBoardConnection = configuration.GetConnectionString("PriceBoardConnection");
        }

        public async Task<List<MyPriceBoardWithPriceDto>> ListMyPriceBoardAsync(string userId)
        {
            var query = @"select mpb.Exchange, mpb.Symbol, pb.CompanyName, pb.Price, mpb.BuyPriceFrom, mpb.BuyPriceTo
from dbo.MyPriceBoard mpb
    join dbo.PriceBoard pb on mpb.Exchange = pb.Exchange and mpb.Symbol = pb.Symbol    
where mpb.UserId = @UserId";

            using (var connection = new SqlConnection(_priceBoardConnection))
            {
                var result = await connection.QueryAsync<MyPriceBoardWithPriceDto>(query, new { UserId = userId });
                return result.ToList();
            }
        }
    }
}
