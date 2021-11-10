using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace WebApplication1.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            string test = "<td class='txtl has-tooltip company - tooltip has - symbol ' data-tooltip='Công ty cổ phần Nhựa An Phát Xanh'> <a class='symbol txt-lime' id='AAAsym' onclick='openStockDetail(&quot; AAA & quot;)'><span class='has - symbol'>AAA</span></a>  </td>";

            var searchKey = "data-tooltip";
            var index1 = test.IndexOf(searchKey) + searchKey.Length + 2;
            var index2 = test.IndexOf("'", index1);
            var result = test.Substring(index1, index2 - index1);
            var kasdhk = "cscsdc";
        }
    }
}
