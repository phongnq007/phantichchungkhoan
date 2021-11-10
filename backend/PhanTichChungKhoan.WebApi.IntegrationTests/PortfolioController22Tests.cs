using Microsoft.AspNetCore.Mvc.Testing;
using PhanTichChungKhoan.WebApi.IntegrationTests.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace PhanTichChungKhoan.WebApi.IntegrationTests
{
    public class PortfolioController22Tests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        //private readonly CustomWebApplicationFactory<Startup> _factory;

        public PortfolioController22Tests(CustomWebApplicationFactory<Startup> factory)
        {
            //_factory = factory;
            _client = factory.CreateClient();
            //_client = factory.CreateClient(new WebApplicationFactoryClientOptions
            //{
            //    AllowAutoRedirect = false
            //});
        }

        [Fact]
        public async Task Test()
        {
            //var client = await CreateClientAsync();
            //_priceBoardDbContext.Set<MyPriceBoard>().Add(new MyPriceBoard
            //{
            //    BuyPriceFrom = 0,
            //    BuyPriceTo = 0,
            //    Exchange = "",
            //    Symbol = "",
            //    UserId = ""
            //});
            //await _priceBoardDbContext.SaveChangesAsync();

            //api/portfolio/get-buying-range
            var response = await _client.GetAsync("/api/portfolio/get-buying-range");

            response.EnsureSuccessStatusCode();
        }
    }
}
