using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Model;
using QuerySQLDapperMicroService.Config;

namespace QuerySQLDapperMicroService.Repository
{
    public class AirportRepository : IAirportRepository
    {

        private string _connection;
        public AirportRepository()
        {

            _connection = DatabaseSettings.Get();

        }

        public AirportSQL GetById(int id)
        {

            try
            {

                using (var db = new SqlConnection(_connection))
                {
                    db.Open();
                    var airport = db.QueryFirst<AirportSQL>(AirportSQL.GETBYID, new { Id = id });
                    return (AirportSQL)airport;
                }

            }
            catch
            { 
                return null;
            }


        }

        public AirportSQL GetByCode(string code)
        {

            try
            {

                using (var db = new SqlConnection(_connection))
                {
                    db.Open();
                    var airport = db.QueryFirst<AirportSQL>(AirportSQL.GETBYCODE, new { Code = code });
                    return (AirportSQL)airport;
                }

            }
            catch
            {
                return null;
            }


        }

    }
}
