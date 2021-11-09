using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PhanTichChungKhoan.Application.DTO;
using PhanTichChungKhoan.Application.Interfaces;

namespace PhanTichChungKhoan.WebApp.Pages
{
    [Authorize]
    public class EditBuyingRangeModel : PageModel
    {
        [BindProperty]
        public MyPriceBoardWithPriceDto BuyingRange { get; set; }

        private readonly IMyPriceBoardUsecase _myPriceBoardUsecase;

        public EditBuyingRangeModel(IMyPriceBoardUsecase myPriceBoardUsecase)
        {
            _myPriceBoardUsecase = myPriceBoardUsecase;
        }

        public async Task<IActionResult> OnGetAsync(string exchange, string symbol)
        {
            if (string.IsNullOrEmpty(exchange) || string.IsNullOrEmpty(symbol))
            {
                return NotFound();
            }

            BuyingRange = await _myPriceBoardUsecase.GetBuyingRangeByKey(exchange, symbol);

            if (BuyingRange == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _myPriceBoardUsecase.UpdateBuyingRange(BuyingRange);

            return RedirectToPage("./Portfolio");
        }
    }
}
