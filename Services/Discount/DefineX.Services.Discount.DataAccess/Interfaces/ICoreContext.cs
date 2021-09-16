﻿using DefineX.Services.Discount.DataAccess.Concrete.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefineX.Services.Discount.DataAccess.Interfaces
{
    public interface ICoreContext
    {
        public string DbCon { get; }
        public string MongoConnectionString { get;  }
        public RedisSettings RedisSettings { get; }
    }
}
