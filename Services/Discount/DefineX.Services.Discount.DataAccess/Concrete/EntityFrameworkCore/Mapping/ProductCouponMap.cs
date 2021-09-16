using DefineX.Services.Discount.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefineX.Services.Discount.DataAccess.Concrete.EntityFrameworkCore.Mapping
{
    class ProductCouponMap : IEntityTypeConfiguration<ProductCoupon>
    {
        public void Configure(EntityTypeBuilder<ProductCoupon> builder)
        {
            builder.HasKey(I => I.Id);
            builder.Property(I => I.Id).UseIdentityColumn();

        

            builder
                 .HasOne<Product>(sc => sc.Product)
                .WithMany(s => s.ProductCoupons)
                .HasForeignKey(sc => sc.ProductId);


            builder
                 .HasOne<Coupon>(sc => sc.Coupon)
                .WithMany(s => s.ProductCoupons)
                .HasForeignKey(sc => sc.CouponId);

        }
    }
}
