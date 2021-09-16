using DefineX.Services.Discount.DataAccess.Concrete.EntityFrameworkCore.Context;
using DefineX.Services.Discount.DataAccess.Interfaces;
using DefineX.Services.Discount.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DefineX.Services.Discount.DataAccess.Concrete.EntityFrameworkCore.Repositories
{
    public class EfProductRepository : EfGenericRepository<Product>, IProductDal
    {
        private readonly DefineXContext _context;
        private readonly IGenericDal<ProductCoupon> _genericProductCouponDal;
        private readonly IGenericDal<Product> _genericProductDal;
        public EfProductRepository(DefineXContext context, IGenericDal<ProductCoupon> genericProductCoupontDal, IGenericDal<Product> genericProductDal) : base(context)
        {
            _context = context;
            _genericProductCouponDal = genericProductCoupontDal;
            _genericProductDal = genericProductDal;
        }
        public async Task<List<Product>> GetProductAsync()
        {
            return await _context.Products.Include(x => x.ProductCoupons).ThenInclude(x => x.Coupon).ToListAsync();
        }
        public async Task<Product> AddProductAsync(Product product, List<int> coupons)
        {
            var trans = _context.Database.BeginTransaction();
            try
            {
                _context.Products.Add(product);
                _context.ProductCoupons.RemoveRange(_context.ProductCoupons.Where(x => x.ProductId == product.Id));
                await _context.SaveChangesAsync();
                foreach (var item in coupons)
                {
                    _context.ProductCoupons.Add(new ProductCoupon { ProductId = product.Id , CouponId = item });
                    await _context.SaveChangesAsync();
                }
                trans.Commit();
                return product;
            }
            catch (Exception ex)
            {
                trans.Rollback();
                return null;
            }
        }
        public async Task<Product> UpdateProductAsync(Product product, List<int> coupons)
        {
            var trans = _context.Database.BeginTransaction();
            try
            {
                await _genericProductDal.UpdateAsync(product);
                await _context.SaveChangesAsync();
                var couponsList = await _context.ProductCoupons.Where(x => x.ProductId == product.Id).ToListAsync();
                foreach (var item in couponsList)
                {
                    item.IsDeleted = true;
                   // _context.Entry(item).State = EntityState.Modified;
                    await _genericProductCouponDal.UpdateAsync(item);
                }                
                foreach (var item in coupons)
                {
                    _context.ProductCoupons.Add(new ProductCoupon { ProductId = product.Id, CouponId = item });
                    await _context.SaveChangesAsync();
                }
                trans.Commit();
                return product;
            }
            catch (Exception ex)
            {
                trans.Rollback();
                return null;
            }
        }

        public async Task<Product> GetProductAsync(Expression<Func<Product, bool>> predicate)
        {
            var result = _context.Products.Where(predicate).AsQueryable();
            return await result.Include(x => x.ProductCoupons).FirstOrDefaultAsync();
        }
    }
}
