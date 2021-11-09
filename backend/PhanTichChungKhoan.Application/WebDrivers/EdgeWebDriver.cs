using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;
using System;

namespace PhanTichChungKhoan.Application.WebDrivers
{
    public class EdgeWebDriver : SeleniumWebDriver
    {
        public EdgeWebDriver(ILogger<VnDirectCrawler> logger) : base(logger)
        {
            var opt = new EdgeOptions()
            {
                PageLoadStrategy = PageLoadStrategy.Normal,
                AcceptInsecureCertificates = true
            };

            _webDriver = new EdgeDriver($@"{Environment.CurrentDirectory}\EdgeWebDriver", opt);
            _webDriverWait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(30));
        }
    }
}
