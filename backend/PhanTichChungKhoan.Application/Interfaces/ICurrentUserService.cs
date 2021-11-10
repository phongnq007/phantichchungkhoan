using System;
using System.Collections.Generic;
using System.Text;

namespace PhanTichChungKhoan.Application.Interfaces
{
    public interface ICurrentUserService
    {
        string Name { get; set; }
        string UserId { get; set; }

    }
}
