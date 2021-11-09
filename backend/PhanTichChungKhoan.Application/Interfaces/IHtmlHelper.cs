using System;
using System.Collections.Generic;
using System.Text;

namespace PhanTichChungKhoan.Application.Interfaces
{
    public interface IHtmlHelper
    {
        IList<string> GetTextByElementSelector(string selector);

    }
}
