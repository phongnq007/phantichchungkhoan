using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PhanTichChungKhoan.Domain
{
    public class PriceBoardTemp: EntityBase
    {
        public string Exchange { get; set; }
        public string Symbol { get; set; }
        public double Price { get; set; }
        public string CompanyName { get; set; }
    }
}
