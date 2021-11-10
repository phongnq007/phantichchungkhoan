using System.Threading.Tasks;
using Xunit;

namespace PhanTichChungKhoan.WebApi.IntegrationTests
{
    public class PortfolioControllerTests : Config.InMemoryTestBase
    {
        [Fact]
        public async Task Test()
        {
            var client = await CreateClientAsync();
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
            var response = await client.GetAsync("/api/portfolio/get-buying-range");

            response.EnsureSuccessStatusCode();
        }
    }
}
