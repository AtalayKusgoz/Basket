using DefineX.Services.Discount.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefineX.Services.Discount.Entities.Concrete
{
    public class ProductCoupon : AuditableEntity
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int CouponId { get; set; }
        public Coupon Coupon { get; set; }
    }
}
