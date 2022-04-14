using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AirportMicroService.Config;
using Model;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace AirportMicroService.Services
{
    public class AirportService
    {

        private readonly IMongoCollection<Airport> _airport;

        public AirportService(IAirportDatabaseSettings settings)
        {

            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _airport = database.GetCollection<Airport>(settings.AirportCollectionName);

        }

        public List<Airport> Get() =>
            _airport.Find(airport => true).ToList();

        public Airport Get(string id) =>
            _airport.Find<Airport>(airport => airport.Id == id).FirstOrDefault();

        public Airport GetIATA_Code(string IATA_Code) =>
            _airport.Find<Airport>(airportFound => airportFound.IATA_Code == IATA_Code).FirstOrDefault();


        public async Task<Airport> Create(Airport airport)
        {

            if (airport.UserLogin == null)
            {

                return null;

            }

            var user = await SearchUser.FindUserAsync(airport.UserLogin);

            if (user == null)
            {

                return null;

            }
            if (user.Role != "Admin")
            {

                return null;
            }

             var airportFound = GetIATA_Code(airport.IATA_Code);

            if (airportFound == null)
            {

                _airport.InsertOne(airport);

                Log log = new();
                log.User = user;
                log.EntityBefore = "";
                log.EntityAfter = JsonConvert.SerializeObject(airport);
                log.Operation = "create";
                log.Date = DateTime.Now.Date;

                var check = await InsertLog.InsertLogAsync(log);

                if (check != "Ok")
                {

                    _airport.DeleteOne(airportIn => airportIn.Id == airport.Id);
                    return null;

                }

                return airport;

            }

            return null;
          

        }

        public async Task<Airport> Update(string id, Airport airport_updated)
        {

            if (airport_updated.UserLogin == null)
            {

                return null;

            }

            var user = await SearchUser.FindUserAsync(airport_updated.UserLogin);

            if (user == null)
            {

                return null;

            }
            if (user.Role != "Admin")
            {

                return null;

            }

            var airport = Get(id);

            _airport.ReplaceOne(airportIn => airportIn.Id == id, airport_updated);


            Log log = new();
            log.User = user;
            log.EntityBefore = JsonConvert.SerializeObject(airport);
            log.EntityAfter = JsonConvert.SerializeObject(airport_updated);
            log.Operation = "update";
            log.Date = DateTime.Now.Date;

            var check = await InsertLog.InsertLogAsync(log);

            if (check != "Ok")
            {

                _airport.ReplaceOne(airportIn => airportIn.Id == airport_updated.Id, airport);
                return null;

            }

            return airport_updated;

        }

        public void Remove(Airport airportToRemove) =>
            _airport.DeleteOne(airport => airport.Id == airportToRemove.Id);

        public async Task<User> Remove(string id, User user)
        {

            if (user.UserLogin == null)
            {

                return null;

            }

            user = await SearchUser.FindUserAsync(user.UserLogin);

            if (user == null)
            {

                return null;

            }
            if (user.Role != "Admin")
            {

                return null;

            }

            var airport = Get(id);

            _airport.DeleteOne(airportIn => airportIn.Id == id);

            Log log = new();
            log.User = user;
            log.EntityBefore = JsonConvert.SerializeObject(airport);
            log.EntityAfter = "";
            log.Operation = "delete";
            log.Date = DateTime.Now.Date;

            var check = await InsertLog.InsertLogAsync(log);

            if (check != "Ok")
            {

                _airport.InsertOne(airport);
                return null;

            }


            return user;

        }

    }
}
