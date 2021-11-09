using PhanTichChungKhoan.Application.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PhanTichChungKhoan.Application.Interfaces
{
    public interface IWebCrawler
    {
        Task ExtractDataAsync(ExchangeSymbol exchange);

    }
}
