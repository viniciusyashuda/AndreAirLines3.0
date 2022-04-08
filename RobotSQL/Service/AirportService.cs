using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using RobotSQL.Repository;

namespace RobotSQL.Service
{
    public class AirportService
    {

        private IAirportRepository _airport;

        public AirportService()
        {

            _airport = new AirportRepository();

        }

        public bool Add(AirportSQL airport)
        {

            return _airport.Add(airport);

        }

    }
}
