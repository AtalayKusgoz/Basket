using DefineX.Services.Discount.Business.Interfaces;
using DefineX.Services.Discount.DataAccess.Concrete.Models;
using DefineX.Services.Discount.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefineX.Services.Discount.Business.Concrete
{
    public class MongoManager : IMongoService
    {
        private readonly IMongoLog _mongoLog;
        public MongoManager(IMongoLog mongoLog) 
        {
            _mongoLog = mongoLog;
        }

        public async Task LogEkle(MongoLogModel mongoLogModel)
        {
            await _mongoLog.LogEkle(mongoLogModel);
        }
    }
}
