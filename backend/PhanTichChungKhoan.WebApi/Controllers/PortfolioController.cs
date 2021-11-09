using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhanTichChungKhoan.Application.DTO;
using PhanTichChungKhoan.Application.Interfaces;

namespace PhanTichChungKhoan.WebApi.Controllers
{
    [Route("api/portfolio")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PortfolioController : ControllerBase
    {
        private readonly IMyPriceBoardUsecase _myPriceBoard;

        public PortfolioController(IMyPriceBoardUsecase myPriceBoard)
        {
            _myPriceBoard = myPriceBoard;
        }

        [ProducesResponseType(typeof(MyPriceBoardWithPriceDto), (int)HttpStatusCode.OK)]
        [HttpGet("get-buying-range")]
        public async Task<IActionResult> GetBuyingRange()
        {
            var listdata = await _myPriceBoard.ListMyPriceBoard();
            return Ok(listdata);
        }
        
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [HttpPost("add-buying-range")]
        public async Task<IActionResult> AddBuyingRange(MyPriceBoardWithPriceDto buyingRangeDto)
        {
            await _myPriceBoard.AddBuyingRange(buyingRangeDto);
            return Ok();
        }
    }
}
