using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Model
{
    public class Ticket
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

        public Flight Flight { get; set; }

        public Passenger Passenger { get; set; }

        public Class Class { get; set; }

        public BasePrice BasePrice { get; set; }

        public double SalePercentage { get; set; }

        public double TotalValue { get; set; }

        public DateTime RegistrationDate { get; set; }

        public string UserLogin { get; set; }

    }
}
