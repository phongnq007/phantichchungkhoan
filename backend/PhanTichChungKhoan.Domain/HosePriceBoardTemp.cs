using System;
using System.Collections.Generic;
using System.Text;

namespace PhanTichChungKhoan.Domain
{
    public class HosePriceBoardTemp : EntityBase
    {
        public string Exchange { get; set; }
        public string Symbol { get; set; }
        public double Price { get; set; }
        public string CompanyName { get; set; }

        public static IEnumerable<HosePriceBoardTemp> FromPriceBoardTemp(List<PriceBoardTemp> items)
        {
            //if (items is null || items.Count < 1)
            //{
            //    return new List<HosePriceBoardTemp>();
            //}

            foreach (var item in items)
            {
                yield return new HosePriceBoardTemp
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
