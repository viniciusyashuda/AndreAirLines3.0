using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using QuerySQLDapperMicroService.Repository;

namespace QuerySQLDapperMicroService.Service
{
    public class AirportService
    {

        private IAirportRepository _airport;

        public AirportService()
        {

            _airport = new AirportRepository();

        }


        public AirportSQL GetById(int id)
        {

            return _airport.GetById(id);

        }

        public AirportSQL GetByCode(string code)
        {

            return _airport.GetByCode(code);

        }

    }
}
