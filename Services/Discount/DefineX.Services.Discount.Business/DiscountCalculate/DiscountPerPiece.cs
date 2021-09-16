using DefineX.Services.Discount.Business.Interfaces;
using DefineX.Services.Discount.DataAccess.Concrete.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefineX.Services.Discount.Business.DiscountCalculate
{
    public class DiscountPerPiece : ICalculate
    {
        public BasketCoupon BasketCalculate(BasketItem basketItem)
        {
            int condition = 3;
            int coupunId = (int)DiscountEnum.DiscountPerPiece;
            if (basketItem.Coupons.Any(f => f.CouponId == coupunId && basketItem.Quantity >= condition))
            {
                int check = basketItem.Quantity / condition;
                return new BasketCoupon
                {
                    ProductId = basketItem.ProductId,
                    CouponId = (int)DiscountEnum.DiscountPerPiece,
                    DiscountPrice = (basketItem.Price * check * condition * 15) / 100,
                };
            }
            return null;
        }
    }
}
