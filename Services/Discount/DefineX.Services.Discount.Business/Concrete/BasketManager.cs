using AutoMapper;
using DefineX.Services.Discount.Business.DiscountCalculate;
using DefineX.Services.Discount.Business.Interfaces;
using DefineX.Services.Discount.DataAccess.Concrete.Models;
using DefineX.Services.Discount.DataAccess.Interfaces;
using DefineX.Services.Discount.Entities.Concrete;
using Shared.ControllerBases;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefineX.Services.Discount.Business.Concrete
{
    public class BasketManager : IBasketService
    {
        private readonly IProductDal _productDal;
        private readonly IRedisDataService _redisDataService;
        private readonly IMapper _mapper;
        public BasketManager(IProductDal productDal, IRedisDataService redisDataService, IMapper mapper)
        {
            _productDal = productDal;
            _redisDataService = redisDataService;
            _mapper = mapper;
        }
        public async Task<Response<List<BasketItemDto>>> BasketOperations(List<BasketDto> basketDtoList)
        {
            List<BasketItem> basketItems = new List<BasketItem>();
            List<BasketCoupon> basketCoupons = new List<BasketCoupon>();
            foreach (var item in basketDtoList)
            {
                BasketItem mapData;
                var redisData = await _redisDataService.GetProduct(item.ProductId);
                if (redisData == null)
                {
                    var data = await _productDal.GetProductAsync(x => x.Id == item.ProductId);
                    mapData = _mapper.Map<BasketItem>(item);
                    mapData.Price = data.Price;
                    mapData.Coupons = data.ProductCoupons;
                    basketItems.Add(mapData);
                }
                else
                {
                    mapData = _mapper.Map<BasketItem>(item);
                    mapData.Price = redisData.Price;
                    mapData.Coupons = redisData.ProductCoupons;
                    basketItems.Add(mapData);
                }
                basketCoupons.Add(DiscountCalculate(new DiscountPerPiece(), mapData));
                basketCoupons.Add(DiscountCalculate(new DiscountBuOneSecondFree(), mapData));
            }
            basketCoupons.Add(DiscountTotalCalculate(new DiscountTotalAmount(), basketItems));
            SelectProductTApplyDiscount(basketCoupons, basketItems);
            return Response<List<BasketItemDto>>.Success(_mapper.Map<List<BasketItemDto>>(basketItems), 200, basketItems.Count);
        }
        private void SelectProductTApplyDiscount(List<BasketCoupon> basketCoupons, List<BasketItem> basketItems)
        {
            var resultData = basketCoupons.Where(x => x != null)
                .GroupBy(x => new { x.ProductId })
                .Select(cl => new BasketCoupon
                {
                    ProductId = cl.First().ProductId,
                    DiscountPrice = cl.Max(c => c.DiscountPrice),
                    CouponId = cl.First(f => f.DiscountPrice == cl.Max(c => c.DiscountPrice)).CouponId,
                }).ToList();

            if (resultData.Any())
            {
                var dataf = resultData.OrderBy(x => x.DiscountPrice).FirstOrDefault();
                basketItems.Where(w => w.ProductId == dataf.ProductId).Select(s => { s.AppliedCouponId = dataf.CouponId; s.DiscountPrice = dataf.DiscountPrice; return s; }).ToList();
            }
        }
        private BasketCoupon DiscountCalculate(ICalculate calculate, BasketItem basketItem)
        {
            return calculate.BasketCalculate(basketItem);
        }
        private BasketCoupon DiscountTotalCalculate(ICalculateTotal calculate, List<BasketItem> basketItemList)
        {
            return calculate.BasketTotalCalculate(basketItemList);
        }
    }
}
