using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace Model
{
    public class User : Person
    {

        public string Login { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }

        public Occupation Occupation { get; set; }

    }
}
