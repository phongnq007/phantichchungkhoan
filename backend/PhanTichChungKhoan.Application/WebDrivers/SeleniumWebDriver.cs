using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using PhanTichChungKhoan.Application.Enums;
using PhanTichChungKhoan.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace PhanTichChungKhoan.Application.WebDrivers
{
    public abstract class SeleniumWebDriver : IDisposable
    {
        protected bool disposedValue;
        protected IWebDriver _webDriver;
        protected WebDriverWait _webDriverWait;
        protected readonly ILogger<VnDirectCrawler> _logger;

        public List<PriceBoardTemp> ListPriceBoard { get; private set; }

        protected SeleniumWebDriver(ILogger<VnDirectCrawler> logger)
        {
            _logger = logger;
            ListPriceBoard = new List<PriceBoardTemp>();
        }

        public async Task GetListPriceBoard(string url, ExchangeSymbol exchange)
        {
            var listSymbolError = new List<string>();
            ListPriceBoard.Clear();

            _webDriver.Navigate().GoToUrl(url);
            _webDriverWait.Until(d => d.FindElement(By.CssSelector("tbody#banggia-khop-lenh-body")).Displayed);

            var listElem = _webDriver.FindElements(By.CssSelector("tbody#banggia-khop-lenh-body tr"));

            _logger.LogInformation($"Number of tr elements crawled on {exchange} : {listElem.Count}.");

            var symbol = string.Empty;
            var priceText = string.Empty;
            var company = string.Empty;
            var companySearchKey = "data-tooltip";

            foreach (var item in listElem)
            {
                var firstCol = item.FindElement(By.CssSelector("td:first-child"));

                var strHtml = firstCol.GetAttribute("outerHTML").Trim();
                var index1 = strHtml.IndexOf(companySearchKey) + companySearchKey.Length + 2;
                var index2 = strHtml.IndexOf('"', index1);

                company = HttpUtility.HtmlDecode(strHtml.Substring(index1, index2 - index1));
                symbol = firstCol.GetAttribute("innerText").Trim();
                priceText = item.FindElement(By.CssSelector("span[id$='matchP']")).GetAttribute("innerText").Trim();

                if (!string.IsNullOrWhiteSpace(symbol))
                {
                    if (double.TryParse(priceText, out var dummy))
                    {
                        ListPriceBoard.Add(new PriceBoardTemp
                        {
                            Exchange = exchange.ToString(),
                            Price = Convert.ToDouble(priceText),
                            Symbol = symbol.ToUpper(),
                            CompanyName = company
                        });
                    }
                    else
                    {
                        listSymbolError.Add(symbol);
                    }
                }
            }

            if (listSymbolError.Count > 0)
            {
                _logger.LogInformation($"{exchange} - Could not get price of symbols: {string.Join(", ", listSymbolError)}");
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    ListPriceBoard.Clear();
                    _webDriver?.Close();
                    _webDriver?.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                _webDriverWait = null;
                ListPriceBoard = null;
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
