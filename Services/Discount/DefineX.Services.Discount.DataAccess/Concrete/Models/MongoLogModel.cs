using Shared.ControllerBases;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefineX.Services.Discount.DataAccess.Concrete.Models
{
    public class MongoLogModel 
    {
        public ObjectId Id { get; set; }

        [BsonElement("TypeId")]
        public string LogType { get; set; }

        [BsonElement("Path")]
        public string Path { get; set; }

        [BsonElement("Data")]
        public string Data { get; set; }

        [BsonElement("Date")]
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
