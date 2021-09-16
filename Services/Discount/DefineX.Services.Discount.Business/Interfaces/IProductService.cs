using Shared.ControllerBases;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefineX.Services.Discount.Business.Interfaces
{
    public interface IProductService
    {
        Task<Response<List<GetProductDto>>> GetProductAsync();
        Task<Response<ProductAddDto>> AddProductAsync(ProductAddDto productAddDto);
        Task<Response<ProductUpdateDto>> UpdateProductAsync(ProductUpdateDto productUpdateDto);
    }
}
