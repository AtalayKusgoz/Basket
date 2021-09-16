using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefineX.Services.Discount.DataAccess.Concrete.Models
{
    public class BasketCoupon
    {
        public int ProductId { get; set; }
        public decimal DiscountPrice { get; set; }
        public int CouponId { get; set; }
    }
}
