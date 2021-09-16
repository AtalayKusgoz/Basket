using DefineX.Services.Discount.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefineX.Services.Discount.Entities.Concrete
{
    public class Coupon : AuditableEntity
    {
        public string Name { get; set; }
        public List<ProductCoupon> ProductCoupons { get; set; }
    }
}
