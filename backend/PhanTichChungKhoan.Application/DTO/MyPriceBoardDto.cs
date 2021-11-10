using PhanTichChungKhoan.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhanTichChungKhoan.Application.DTO
{
    public class MyPriceBoardDto
    {
        public string Exchange { get; set; }
        public string Symbol { get; set; }
        public double Price { get; set; }
        public double BuyingFrom { get; set; }
        public double BuyingTo { get; set; }
        public string BuyingStatus { get; set; }

       
    }
}
