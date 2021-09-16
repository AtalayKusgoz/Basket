using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefineX.Services.Discount.Entities.Interfaces
{
    public interface ITable
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
    }
}
