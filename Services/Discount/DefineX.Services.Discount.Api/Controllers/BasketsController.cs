using DefineX.Services.Discount.Business.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.ControllerBases;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DefineX.Services.Discount.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [EnableCors]
    public class BasketsController : CustomBaseController
    {
        private readonly IBasketService _basketService;

        public BasketsController(IBasketService basketService)
        {
            _basketService = basketService;
        }
        [HttpPost]
        public async Task<IActionResult> BasketOperation(List<BasketDto> basketDtoList)
        {
            return CreateActionResultInstance(await _basketService.BasketOperations(basketDtoList));
        }
    }
}
