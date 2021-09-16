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
    public class ProductsController : CustomBaseController
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            return CreateActionResultInstance(await _productService.GetProductAsync());
        }
        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductAddDto productAddDto)
        {
            return CreateActionResultInstance(await _productService.AddProductAsync(productAddDto));
        }
        [HttpPut]
        public async Task<IActionResult> UpdateProduct(ProductUpdateDto productUpdateDto)
        {
            return CreateActionResultInstance(await _productService.UpdateProductAsync(productUpdateDto));
        }
    }
}
