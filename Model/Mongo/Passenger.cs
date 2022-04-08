using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Model
{
    public class Passenger
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } =  ObjectId.GenerateNewId().ToString();

        public string Cpf { get; set; }

        public string Passenger_Name { get; set; }

        public string Phone { get; set; }

        public DateTime BirthDate { get; set; }

        public string Email { get; set; }

        public virtual Address Address { get; set; }


    }
}
