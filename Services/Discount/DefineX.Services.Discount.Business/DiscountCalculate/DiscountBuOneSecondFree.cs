using DefineX.Services.Discount.Business.Interfaces;
using DefineX.Services.Discount.DataAccess.Concrete.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefineX.Services.Discount.Business.DiscountCalculate
{
    public class DiscountBuOneSecondFree : ICalculate
    {
        public BasketCoupon BasketCalculate(BasketItem basketItem)
        {
            int condition = 2;
            int coupunId = (int)DiscountEnum.DiscountBuOneSecondFree;
            if (basketItem.Coupons.Any(f => f.CouponId == coupunId && basketItem.Quantity >= condition))
            {
                return new BasketCoupon
                {
                    ProductId = basketItem.ProductId,
                    CouponId = (int)DiscountEnum.DiscountBuOneSecondFree,
                    DiscountPrice = basketItem.Price
                };

            }
            return null;
        }
    }
}
