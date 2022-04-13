using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Model
{
    public class BasePrice
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } =  ObjectId.GenerateNewId().ToString();

        public Airport Origin { get; set; }

        public Airport Destination { get; set; } 

        public double Value { get; set; }

        public DateTime InclusionDate { get; set; }

        public string UserLogin { get; set; }

    }
}
