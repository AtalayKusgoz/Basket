using DefineX.Services.Discount.Business.Interfaces;
using DefineX.Services.Discount.DataAccess.Concrete.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefineX.Services.Discount.Business.DiscountCalculate
{
    public class DiscountTotalAmount : ICalculateTotal
    {
        public BasketCoupon BasketTotalCalculate(List<BasketItem> sepetList)
        {
            var totalPrice = sepetList.Sum(x => x.TotalPrice);
            if (totalPrice >= 1000)
            {
                var result = sepetList.OrderBy(x => x.Price).FirstOrDefault();
                return new BasketCoupon
                {
                    ProductId = result.ProductId,
                    CouponId = (int)DiscountEnum.DiscountTotalAmount,
                    DiscountPrice = result.Price  / 5, // %20
                };
            }
            else
            {
                return null;
            }
        }
    }
}
