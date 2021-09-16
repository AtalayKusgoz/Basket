using DefineX.Services.Discount.Entities.Concrete;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefineX.Services.Discount.DataAccess.Concrete.Models
{
    public class BasketItem
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public List<ProductCoupon> Coupons { get; set; } = new List<ProductCoupon>();
        public decimal TotalPrice
        {
            get
            {
                decimal totalprice = 0;
                totalprice = Price * Quantity;
                return totalprice;
            }
        }
        public decimal DiscountPrice { get; set; }
        public int AppliedCouponId { get; set; }
        public decimal AmountToBePaid { get; set; }
    }
}
