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

        public List<AirportSQL> GetAll()
        {
            try
            {

                using (var db = new SqlConnection(_connection))
                {
                    db.Open();
                    var airports = db.Query<AirportSQL>(AirportSQL.GETALL);
                    return (List<AirportSQL>)airports;
                }

            }
            catch
            {

                Console.WriteLine("\nNo data was found! Check if there is any data inserted in your database!");
                return null;
                
            }


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
                Console.WriteLine("\nNo data with this id was found! Check if the Id inserted is correct and if there is any data inserted in your database!");
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
                Console.WriteLine("\nNo data with this id was found! Check if the Code inserted is correct and if there is any data inserted in your database!");
                return null;
            }


        }

    }
}
