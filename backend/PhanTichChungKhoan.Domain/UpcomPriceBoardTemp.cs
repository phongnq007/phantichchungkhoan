using System;
using System.Collections.Generic;
using System.Text;

namespace PhanTichChungKhoan.Domain
{
    public class UpcomPriceBoardTemp : EntityBase
    {
        public string Exchange { get; set; }
        public string Symbol { get; set; }
        public double Price { get; set; }
        public string CompanyName { get; set; }

        public static IEnumerable<UpcomPriceBoardTemp> FromPriceBoardTemp(List<PriceBoardTemp> items)
        {
            foreach (var item in items)
            {
                yield return new UpcomPriceBoardTemp
                {
                    CompanyName = item.CompanyName,
                    Exchange = item.Exchange,
                    Price = item.Price,
                    Symbol = item.Symbol,
                    UpdatedDate = item.UpdatedDate
                };
            }
        }
    }
}
