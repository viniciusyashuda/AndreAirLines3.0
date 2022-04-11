using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Model
{
    [NotMapped]
        [Keyless]
    public class Address
    {

        [JsonProperty("cep")]
        public string PostalCode { get; set; }

        [JsonProperty("logradouro")]
        public string Street { get; set; }

        public int Number { get; set; }

        [JsonProperty("bairro")]
        public string District { get; set; }

        [JsonProperty("localidade")]
        public string City { get; set; }

        public string Country { get; set; }

        public string Continent { get; set; }

        [JsonProperty("uf")]
        public string Federative_Unit { get; set; }

        public string Complement { get; set; }

        public Address(string postalCode, string street, int number, string district, string city, string country, string continent, string federative_Unit, string complement)
        {
            PostalCode = postalCode;
            Street = street;
            Number = number;
            District = district;
            City = city;
            Country = country;
            Continent = continent;
            Federative_Unit = federative_Unit;
            Complement = complement;
        }
    }
}
