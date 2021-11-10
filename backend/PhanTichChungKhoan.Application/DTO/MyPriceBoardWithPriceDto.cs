using PhanTichChungKhoan.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace PhanTichChungKhoan.Application.DTO
{
    public class MyPriceBoardWithPriceDto
    {
        /// <example>HOSE</example>
        public string Exchange { get; set; }

        /// <example>ACB</example>
        public string Symbol { get; set; }
        public string CompanyName { get; set; }
        public double Price { get; set; }
        
        [DisplayName("Buy From")]
        public double BuyPriceFrom { get; set; }
        
        [DisplayName("Buy To")]
        public double BuyPriceTo { get; set; }

        [DisplayName("Status")]
        public string BuyingStatus { get; set; }

        public static MyPriceBoard ToMyPriceBoard(MyPriceBoardWithPriceDto item)
        {
            if (item is null)
            {
                throw new ArgumentNullException("MyPriceBoardWithPriceDto is null.");
            }

            return new MyPriceBoard
            {
                BuyPriceFrom = item.BuyPriceFrom,
                BuyPriceTo = item.BuyPriceTo,
                Symbol = item.Symbol.Trim().ToUpper()
            };
        }

        public static MyPriceBoardWithPriceDto FromMyPriceBoard(MyPriceBoard item)
        {
            if (item is null)
            {
                throw new ArgumentNullException("MyPriceBoard is null.");
            }

            return new MyPriceBoardWithPriceDto
            {
                BuyPriceFrom = item.BuyPriceFrom,
                BuyPriceTo = item.BuyPriceTo,
                Symbol = item.Symbol,
                Exchange = item.Exchange
            };
        }
    }
}
