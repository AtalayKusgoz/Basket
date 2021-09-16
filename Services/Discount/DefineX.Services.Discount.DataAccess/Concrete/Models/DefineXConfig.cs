using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefineX.Services.Discount.DataAccess.Concrete.Models
{
    public class DefineXConfig
    {
        public string DbCon { get; set; }
        public string MongoConnectionString { get; set; }
        public RedisSettings RedisSettings { get; set; }
    }
    public class RedisSettings
    {
        public string Host { get; set; }
        public int Port { get; set; }
    }
}
