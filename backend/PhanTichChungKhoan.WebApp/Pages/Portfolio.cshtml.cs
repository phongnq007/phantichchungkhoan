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
    public class PortfolioModel : PageModel
    {
        public IList<MyPriceBoardWithPriceDto> ListBuyingRange { get; set; }

        private readonly IMyPriceBoardUsecase _myPriceBoard;

        public PortfolioModel(IMyPriceBoardUsecase myPriceBoard)
        {
            _myPriceBoard = myPriceBoard;
        }

        public async Task OnGetAsync()
        {
            ListBuyingRange = await _myPriceBoard.ListMyPriceBoard();
        }

        public async Task<IActionResult> OnPostDeleteAsync(string exchange, string symbol)
        {
            if (string.IsNullOrWhiteSpace(exchange) || string.IsNullOrWhiteSpace(symbol))
            {
                return Page();
            }

            await _myPriceBoard.DeleteBuyingRange(exchange, symbol);

            return RedirectToPage("./Portfolio");
        }
    }
}
