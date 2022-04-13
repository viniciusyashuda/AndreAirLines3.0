using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Model
{
    public class Log
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

        public User User { get; set; }

        public string EntityBefore { get; set; }

        public string EntityAfter { get; set; }

        public string Operation { get; set; }

        public DateTime Date { get; set; }

    }
}
