using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WebApplication1.Data
{
    public class MyPriceBoard
    {
        [Key]
        public string UserId { get; set; }
        public string Exchange { get; set; }
        public string Symbol { get; set; }
        
        public double BuyPriceFrom { get; set; }
        public double BuyPriceTo { get; set; }
    }
}
