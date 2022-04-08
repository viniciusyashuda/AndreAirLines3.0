using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class AirportSQL
    {

        public readonly static string INSERT = "INSERT INTO AIRPORT (City, Country, Code, Continent) VALUES (@City, @Country, @Code, @Continent)";

        public int Id { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string Code { get; set; }

        public string Continent { get; set; }

        public AirportSQL(string city, string country, string code, string continent)
        {
            City = city;
            Country = country;
            Code = code;
            Continent = continent;
        }

        public override string ToString()
        {
            return "Id: " + Id 
                + "\nCity: " + City 
                + "\nCountry: " + Country 
                + "\nCode: " + Code 
                + "\nContinent: " + Continent;
        }


    }
}
