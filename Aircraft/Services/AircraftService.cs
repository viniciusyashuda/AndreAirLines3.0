using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AircraftMicroService.Config;
using AircraftMicroService.Services;
using Model;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace AircraftMicroService.Service
{
    public class AircraftService
    {

        private readonly IMongoCollection<Aircraft> _aircraft;

        public AircraftService(IAircraftDatabaseSettings settings)
        {

            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _aircraft = database.GetCollection<Aircraft>(settings.AircraftCollectionName);

        }

        public List<Aircraft> Get() =>
            _aircraft.Find(aircraft => true).ToList();

        public Aircraft Get(string id) =>
            _aircraft.Find<Aircraft>(aircraft => aircraft.Id == id).FirstOrDefault();

        public Aircraft GetRegistration(string registration) =>
            _aircraft.Find<Aircraft>(aircraft => aircraft.Registration == registration).FirstOrDefault();

        public async Task<Aircraft> Create(Aircraft aircraft)
        {

            if(aircraft.UserLogin == null)
            {

                return null;

            }

            var user = await SearchUser.FindUserAsync(aircraft.UserLogin);

            if(user == null)
            {

                return null;

            }
            if(user.Role != "Admin")
            {

                return null;

            }

            var aircraftFound = GetRegistration(aircraft.Registration);

            if(aircraftFound == null)
            {

                _aircraft.InsertOne(aircraft);

                Log log = new();
                log.User = user;
                log.EntityBefore = "";
                log.EntityAfter = JsonConvert.SerializeObject(aircraft);
                log.Operation = "create";
                log.Date = DateTime.Now.Date;

                var check = await InsertLog.InsertLogAsync(log);

                if(check != "Ok")
                {

                    _aircraft.DeleteOne(aircraftIn => aircraftIn.Id == aircraft.Id);
                    return null;

                }

                     return aircraft;

            }

            return null;

        }

        public async Task<Aircraft> Update(string id, Aircraft aircraft_updated)
        {

            if (aircraft_updated.UserLogin == null)
            {

                return null;

            }

            var user = await SearchUser.FindUserAsync(aircraft_updated.UserLogin);

            if (user == null)
            {

                return null;

            }
            if (user.Role != "Admin")
            {

                return null;

            }

            var aircraft = Get(id);

            _aircraft.ReplaceOne(aircraftIn => aircraftIn.Id == id, aircraft_updated);

            Log log = new();
            log.User = user;
            log.EntityBefore = JsonConvert.SerializeObject(aircraft);
            log.EntityAfter = JsonConvert.SerializeObject(aircraft_updated);
            log.Operation = "update";
            log.Date = DateTime.Now.Date;

            var check = await InsertLog.InsertLogAsync(log);

            if (check != "Ok")
            {

                _aircraft.ReplaceOne(aircraftIn => aircraftIn.Id == aircraft_updated.Id, aircraft);
                return null;

            }

            return aircraft_updated;

        }

        public void Remove(Aircraft aircraftToRemove) =>
            _aircraft.DeleteOne(aircraft => aircraft.Id == aircraftToRemove.Id);

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

            var aircraft = Get(id);

            _aircraft.DeleteOne(aircraftIn => aircraftIn.Id == id);

            Log log = new();
            log.User = user;
            log.EntityBefore = JsonConvert.SerializeObject(aircraft);
            log.EntityAfter = "";
            log.Operation = "delete";
            log.Date = DateTime.Now.Date;

            var check = await InsertLog.InsertLogAsync(log);

            if (check != "Ok")
            {

                _aircraft.InsertOne(aircraft);
                return null;

            }

            return user;

        }

    }
}
