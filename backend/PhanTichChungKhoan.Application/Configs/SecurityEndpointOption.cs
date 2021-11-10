using System;
using System.Collections.Generic;
using System.Text;

namespace PhanTichChungKhoan.Application.Configs
{
    public class SecurityEndpointOption
    {
        public VnDirectExchange VnDirect { get; set; }

    }

    public class VnDirectExchange
    {
        public string Hose { get; set; }
        public string Hnx { get; set; }
        public string Upcom { get; set; }
    }
}
