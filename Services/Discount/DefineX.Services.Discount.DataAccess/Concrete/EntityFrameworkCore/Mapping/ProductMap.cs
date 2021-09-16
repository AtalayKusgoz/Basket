using DefineX.Services.Discount.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DefineX.Services.Discount.DataAccess.Concrete.EntityFrameworkCore.Mapping
{
    class ProductMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(I => I.Id);
            builder.Property(I => I.Id).UseIdentityColumn();
            builder.Property(I => I.Name).HasMaxLength(30).IsRequired();
            builder.Property(I => I.Description).HasMaxLength(100);
            builder.Property(I => I.Price).HasPrecision(38, 2);
        }
    }
}
