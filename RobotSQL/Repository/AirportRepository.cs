using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Model;
using RobotSQL.Config;

namespace RobotSQL.Repository
{
    public class AirportRepository : IAirportRepository
    {

        private string _connection;
        public AirportRepository()
        {

            _connection = DatabaseSettings.Get();

        }


        public bool Add(AirportSQL airport)
        {

            bool status = false;

            using (var db = new SqlConnection(_connection))
            {

                db.Open();
                db.Execute(AirportSQL.INSERT, airport);
                status = true;
                db.Close();

            }

            return status;

        }

    }
}
