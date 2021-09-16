using DefineX.Services.Discount.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefineX.Services.Discount.Entities.Base
{
    public class AuditableEntity : IAuditableEntity, ITable
    {
        public int Id { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
    public enum EnumState
    {
        Update = 1,
        Delete = 2,
        Added = 3
    }
}
