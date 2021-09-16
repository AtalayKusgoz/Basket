using DefineX.Services.Discount.DataAccess.Concrete.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefineX.Services.Discount.Business.Interfaces
{
    public interface ICalculateTotal
    {
        BasketCoupon BasketTotalCalculate(List<BasketItem> basketItemList);
    }
}
