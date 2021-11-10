using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;

namespace WebApplication1.Pages
{
    public class TestListModel : PageModel
    {
        private readonly WebApplication1.Data.ApplicationDbContext _context;

        public TestListModel(WebApplication1.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<MyPriceBoard> MyPriceBoard { get;set; }

        public async Task OnGetAsync()
        {
            MyPriceBoard = await _context.MyPriceBoard.ToListAsync();
        }
    }
}
