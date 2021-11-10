using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PhanTichChungKhoan.Application.Interfaces
{
    public interface IPriceBoardEfRepository : IEfRepositoryBase
    {
        Task<int> ExecuteSqlRawAsync(string sql);
        int ExecuteSqlRaw(string sql);

    }
}
