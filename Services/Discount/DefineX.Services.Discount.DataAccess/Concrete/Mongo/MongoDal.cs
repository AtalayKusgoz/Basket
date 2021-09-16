using DefineX.Services.Discount.DataAccess.Concrete.Models;
using DefineX.Services.Discount.DataAccess.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefineX.Services.Discount.DataAccess.Concrete.Mongo
{
    public class MongoDal : IMongoLog
    {
        private readonly IMongoCollection<MongoLogModel> mongoDb;

        public MongoDal(string connectionString, string dbName, string tableName)
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(dbName);
            mongoDb = database.GetCollection<MongoLogModel>(tableName);
        }
        public async Task LogEkle(MongoLogModel mongoLogModel)
        {
            await mongoDb.InsertOneAsync(mongoLogModel);
        }
    }
}
