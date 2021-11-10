using Microsoft.EntityFrameworkCore;
using PhanTichChungKhoan.Application.Interfaces;
using PhanTichChungKhoan.Infrastructure.DbContexts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PhanTichChungKhoan.Infrastructure.Repository
{
    public class PriceBoardEfRepository : EfRepositoryBase, IPriceBoardEfRepository
    {
        public PriceBoardEfRepository(PriceBoardDbContext dbContext)
            : base(dbContext)
        {

        }

        public async Task<int> ExecuteSqlRawAsync(string sql)
        {
            return await _dbContext.Database.ExecuteSqlRawAsync(sql);
        }

        public int ExecuteSqlRaw(string sql)
        {
            return _dbContext.Database.ExecuteSqlRaw(sql);
        }
    }
}
