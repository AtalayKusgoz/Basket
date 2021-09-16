using DefineX.Services.Discount.DataAccess.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefineX.Services.Discount.DataAccess.Concrete.Models
{
    public class CoreContext : ICoreContext
    {
        private readonly IOptions<CoreContext> _defineXConfig;
        public CoreContext(IOptions<CoreContext> defineXConfig)
        {
            _defineXConfig = defineXConfig;
        }
        public string DbCon => _defineXConfig.Value.DbCon;
        public string MongoConnectionString => _defineXConfig.Value.MongoConnectionString;       
        public RedisSettings RedisSettings => _defineXConfig.Value.RedisSettings;


    }
}
