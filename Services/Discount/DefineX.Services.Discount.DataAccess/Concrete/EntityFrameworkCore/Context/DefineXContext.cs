using DefineX.Services.Discount.DataAccess.Concrete.EntityFrameworkCore.Extensions;
using DefineX.Services.Discount.DataAccess.Concrete.EntityFrameworkCore.Mapping;
using DefineX.Services.Discount.DataAccess.Concrete.Models;
using DefineX.Services.Discount.Entities.Base;
using DefineX.Services.Discount.Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DefineX.Services.Discount.DataAccess.Concrete.EntityFrameworkCore.Context
{
    public class DefineXContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public DefineXContext(DbContextOptions options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductMap());
            modelBuilder.ApplyConfiguration(new CouponMap());
            modelBuilder.ApplyConfiguration(new ProductCouponMap());

            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 1,
                Name = "Asus",
                Price = 500,
                Description = "Laptop",
                CreatedDate = DateTime.Now,
                IsDeleted = false,
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 2,
                Name = "Lenovo",
                Price = 800,
                Description = "Business Laptop",
                CreatedDate = DateTime.Now,
                IsDeleted = false,
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 3,
                Name = "Monster",
                Price = 1250,
                Description = "Gaming Laptop",
                CreatedDate = DateTime.Now,
                IsDeleted = false,
            });

            modelBuilder.Entity<Coupon>().HasData(new Coupon
            {
                Id = 1,
                Name = "DiscountPerPiece",
                CreatedDate = DateTime.Now,
                IsDeleted = false,
            });
            modelBuilder.Entity<Coupon>().HasData(new Coupon
            {
                Id = 2,
                Name = "DiscountBuOneSecondFree",
                CreatedDate = DateTime.Now,
                IsDeleted = false,
            });

            modelBuilder.Entity<ProductCoupon>().HasData(new ProductCoupon
            {
                Id = 1,
                ProductId = 1,
                CouponId = 1,
                CreatedDate = DateTime.Now,
                IsDeleted = false,
            });
            modelBuilder.Entity<ProductCoupon>().HasData(new ProductCoupon
            {
                Id = 2,
                ProductId = 2,
                CouponId = 2,
                CreatedDate = DateTime.Now,
                IsDeleted = false,
            });
            modelBuilder.Entity<ProductCoupon>().HasData(new ProductCoupon
            {
                Id = 3,
                ProductId = 3,
                CouponId = 1,
                CreatedDate = DateTime.Now,
                IsDeleted = false,
            });
            modelBuilder.Entity<ProductCoupon>().HasData(new ProductCoupon
            {
                Id = 4,
                ProductId = 3,
                CouponId = 2,
                CreatedDate = DateTime.Now,
                IsDeleted = false,
            });
            modelBuilder.AddGlobalFilter();

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<ProductCoupon> ProductCoupons { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            var auditEntries = OnBeforeSaveChanges();
            var result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
            await OnAfterSaveChanges(auditEntries);
            return result;
        }
        private List<AuditEntry> OnBeforeSaveChanges()
        {
            Product currentUser = null;
           // AppUser currentUser = new AppUser();
            try
            {
               // var UserName = _httpContextAccessor.HttpContext.User.Identity.Name;
               // currentUser = this.AppUsers.FirstOrDefault(x => x.UserName == UserName);
            }
            catch
            {

            }

            ChangeTracker.DetectChanges();
            var auditEntries = new List<AuditEntry>();
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is AuditLog || entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                    continue;

                if (entry.Entity is AuditableEntity)
                {
                    var entity = (AuditableEntity)entry.Entity;
                    if (entry.State == EntityState.Added)
                    {
                        entity.CreatedDate = DateTime.Now;
                        entity.CreatedBy = currentUser != null ? currentUser.Id : 0;
                        entity.IsDeleted = false;
                    }
                    else
                    {
                        base.Entry(entity).Property(x => x.CreatedBy).IsModified = false;
                        base.Entry(entity).Property(x => x.CreatedDate).IsModified = false;
                        entity.UpdatedDate = DateTime.Now;
                        entity.UpdatedBy = currentUser != null ? currentUser.Id : 0;
                    }
                }

                var auditEntry = new AuditEntry(entry);
                auditEntry.TableName = entry.Metadata.GetTableName();
                auditEntry.UserId = currentUser != null ? currentUser.Id : 0;
                auditEntries.Add(auditEntry);

                foreach (var property in entry.Properties)
                {
                    if (property.IsTemporary)
                    {
                        auditEntry.TemporaryProperties.Add(property);
                        continue;
                    }

                    string propertyName = property.Metadata.Name;
                    if (property.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues[propertyName] = property.CurrentValue;
                        continue;
                    }

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            auditEntry.NewValues[propertyName] = property.CurrentValue;
                            auditEntry.State = EnumState.Added;
                            break;

                        case EntityState.Deleted:
                            auditEntry.OldValues[propertyName] = property.OriginalValue;
                            auditEntry.State = EnumState.Delete;
                            break;

                        case EntityState.Modified:
                            if (property.IsModified)
                            {
                                auditEntry.OldValues[propertyName] = property.OriginalValue;
                                auditEntry.NewValues[propertyName] = property.CurrentValue;
                                auditEntry.State = EnumState.Update;
                            }
                            break;
                    }
                }
            }

            foreach (var auditEntry in auditEntries.Where(_ => !_.HasTemporaryProperties))
            {
                AuditLogs.Add(auditEntry.ToAudit());
            }

            return auditEntries.Where(_ => _.HasTemporaryProperties).ToList();
        }
        private Task OnAfterSaveChanges(List<AuditEntry> auditEntries)
        {
            if (auditEntries == null || auditEntries.Count == 0)
                return Task.CompletedTask;

            foreach (var auditEntry in auditEntries)
            {
                foreach (var prop in auditEntry.TemporaryProperties)
                {
                    if (prop.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues[prop.Metadata.Name] = prop.CurrentValue;
                    }
                    else
                    {
                        auditEntry.NewValues[prop.Metadata.Name] = prop.CurrentValue;
                    }
                }
                AuditLogs.Add(auditEntry.ToAudit());
            }
            return SaveChangesAsync();
        }
    }
}
