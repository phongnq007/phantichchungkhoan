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
    public class CreateBuyingRangeModel : PageModel
    {
        [BindProperty]
        public MyPriceBoardWithPriceDto BuyingRange { get; set; }

        private readonly IMyPriceBoardUsecase _myPriceBoardUsecase;

        public CreateBuyingRangeModel(IMyPriceBoardUsecase myPriceBoardUsecase)
        {
            _myPriceBoardUsecase = myPriceBoardUsecase;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _myPriceBoardUsecase.AddBuyingRange(BuyingRange);

            return RedirectToPage("./Portfolio");
        }
    }
}
