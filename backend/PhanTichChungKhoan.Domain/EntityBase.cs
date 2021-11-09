using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PhanTichChungKhoan.Domain
{
    public abstract class EntityBase
    {
        [Column(Order = 999)]
        public DateTimeOffset UpdatedDate { get; set; }

    }
}
