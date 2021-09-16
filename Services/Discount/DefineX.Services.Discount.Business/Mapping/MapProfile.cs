using AutoMapper;
using DefineX.Services.Discount.DataAccess.Concrete.Models;
using DefineX.Services.Discount.Entities.Concrete;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefineX.Services.Discount.Business.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<ProductAddDto, Product>();
            CreateMap<ProductUpdateDto, Product>();

            CreateMap<BasketDto, BasketItem>();

            CreateMap<BasketItem, BasketItemDto>();

            CreateMap<Product, GetProductDto>().ForPath(q => q.Coupons, opt => opt.MapFrom(s => s.ProductCoupons));

            CreateMap<ProductCoupon, CouponDto>().ForPath(q => q.Id, opt => opt.MapFrom(s => s.Coupon.Id))
                .ForPath(q => q.Name, opt => opt.MapFrom(s => s.Coupon.Name));
        }          
    }
}
