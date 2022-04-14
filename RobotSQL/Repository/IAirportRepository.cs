using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace RobotSQL.Repository
{
    public interface IAirportRepository
    {
        bool Add(AirportSQL airport);

        List<AirportSQL> GetAll();

        AirportSQL GetById(int id);

        AirportSQL GetByCode(string code);
    }
}
