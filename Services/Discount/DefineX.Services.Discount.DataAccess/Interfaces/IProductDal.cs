using DefineX.Services.Discount.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DefineX.Services.Discount.DataAccess.Interfaces
{
    public interface IProductDal : IGenericDal<Product>
    {
        Task<List<Product>> GetProductAsync();
        Task<Product> AddProductAsync(Product product, List<int> coupons);
        Task<Product> UpdateProductAsync(Product product, List<int> coupons);
        Task<Product> GetProductAsync(Expression<Func<Product, bool>> predicate);
    }
}
