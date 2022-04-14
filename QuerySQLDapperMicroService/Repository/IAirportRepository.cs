using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace QuerySQLDapperMicroService.Repository
{
    public interface IAirportRepository
    {

        AirportSQL GetById(int id);

        AirportSQL GetByCode(string code);

    }
}
