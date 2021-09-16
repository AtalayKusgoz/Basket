using DefineX.Services.Discount.Entities.Concrete;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefineX.Services.Discount.Business.Interfaces
{
    public interface IRedisDataService
    {
        Task<List<GetProductDto>> GetProducts();
        Task<bool> SaveProducts(List<GetProductDto> getProductDto);
        Task<Product> GetProduct(int productId);
        Task<bool> SaveProduct(Product getProductDto);
    }
}
