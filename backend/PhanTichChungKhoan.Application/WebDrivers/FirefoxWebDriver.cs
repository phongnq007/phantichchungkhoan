using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;


namespace PhanTichChungKhoan.Application.WebDrivers
{
    public class FirefoxWebDriver : SeleniumWebDriver
    {
        public FirefoxWebDriver(ILogger<VnDirectCrawler> logger) : base(logger)
        {
            var opt = new FirefoxOptions()
            {
                PageLoadStrategy = PageLoadStrategy.Normal,
                AcceptInsecureCertificates = true
            };

            _webDriver = new FirefoxDriver($@"{Environment.CurrentDirectory}\FirefoxWebDriver", opt);
            _webDriverWait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(30));
        }
    }
}
