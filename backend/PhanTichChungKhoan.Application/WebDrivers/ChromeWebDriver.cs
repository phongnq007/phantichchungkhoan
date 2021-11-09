using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhanTichChungKhoan.Application.WebDrivers
{
    public class ChromeWebDriver : SeleniumWebDriver
    {
        public ChromeWebDriver(ILogger<VnDirectCrawler> logger) : base(logger)
        {
            var opt = new ChromeOptions()
            {
                PageLoadStrategy = PageLoadStrategy.Normal,
                AcceptInsecureCertificates = true
            };

            _webDriver = new ChromeDriver($@"{Environment.CurrentDirectory}\ChromeWebDriver", opt);
            _webDriverWait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(30));
        }
    }
}
