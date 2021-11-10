using System;
using System.Collections.Generic;
using System.Text;

namespace PhanTichChungKhoan.Domain
{
    public class MyPriceBoard : EntityBase
    {
        public string UserId { get; set; }
        public string Exchange { get; set; }
        public string Symbol { get; set; }
        public double BuyPriceFrom { get; set; }
        public double BuyPriceTo { get; set; }
    }
}
