using DefineX.Services.Discount.DataAccess.Concrete.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefineX.Services.Discount.Business.Interfaces
{
    public interface ICalculate
    {
        BasketCoupon BasketCalculate(BasketItem basketItem);
    }
    public enum DiscountEnum
    {
        DiscountPerPiece = 1,
        DiscountBuOneSecondFree = 2,
        DiscountTotalAmount = 3,
    }
}
