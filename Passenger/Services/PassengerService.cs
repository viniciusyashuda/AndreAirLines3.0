using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Model;
using MongoDB.Driver;
using Newtonsoft.Json;
using PassengerMicroService.Config;

namespace PassengerMicroService.Services
{
    public class PassengerService
    {

        private readonly IMongoCollection<Passenger> _passenger;

        public PassengerService(IPassengerDatabaseSettings settings)
        {

            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _passenger = database.GetCollection<Passenger>(settings.PassengerCollectionName);

        }

        public List<Passenger> Get() =>
            _passenger.Find(passenger => true).ToList();

        public Passenger Get(string id) =>
            _passenger.Find<Passenger>(passenger => passenger.Id == id).FirstOrDefault();

        public Passenger GetCpf(string cpf) =>
            _passenger.Find<Passenger>(passenger => passenger.Cpf == cpf).FirstOrDefault();

        public async Task<Passenger> Create(Passenger passenger)
        {

            if (passenger.UserLogin == null)
            {

                return null;

            }

            var user = await SearchUser.FindUserAsync(passenger.UserLogin);

            if (user == null)
            {

                return null;

            }
            if (user.Role != "Admin" && user.Role != "User")
            {

                return null;

            }


            var passengerFound = GetCpf(passenger.Cpf);

            if (passengerFound == null)
            {

                _passenger.InsertOne(passenger);

                Log log = new();
                log.User = user;
                log.EntityBefore = "";
                log.EntityAfter = JsonConvert.SerializeObject(passenger);
                log.Operation = "create";
                log.Date = DateTime.Now.Date;

                var check = await InsertLog.InsertLogAsync(log);

                if (check != "Ok")
                {

                    _passenger.DeleteOne(passengerIn => passengerIn.Id == passenger.Id);
                    return null;

                }

                return passenger;

            }

            return null;

        }

        public async Task<Passenger> Update(string id, Passenger passenger_updated)
        {

            if (passenger_updated.UserLogin == null)
            {

                return null;

            }

            var user = await SearchUser.FindUserAsync(passenger_updated.UserLogin);

            if (user == null)
            {

                return null;

            }
            if (user.Role != "Admin" && user.Role != "User")
            {

                return null;

            }

            var passenger = Get(id);

            _passenger.ReplaceOne(passengerIn => passengerIn.Id == id, passenger_updated);

            Log log = new();
            log.User = user;
            log.EntityBefore = JsonConvert.SerializeObject(passenger);
            log.EntityAfter = JsonConvert.SerializeObject(passenger_updated);
            log.Operation = "update";
            log.Date = DateTime.Now.Date;

            var check = await InsertLog.InsertLogAsync(log);

            if (check != "Ok")
            {

                _passenger.ReplaceOne(flightIn => flightIn.Id == passenger_updated.Id, passenger);
                return null;

            }

            return passenger_updated;

        }

        public void Remove(Passenger passengerToRemove) =>
            _passenger.DeleteOne(passenger => passenger.Id == passengerToRemove.Id);

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
            if (user.Role != "Admin" && user.Role != "User")
            {

                return null;

            }

            var passenger = Get(id);

            _passenger.DeleteOne(passengerIn => passengerIn.Id == id);

            Log log = new();
            log.User = user;
            log.EntityBefore = JsonConvert.SerializeObject(passenger);
            log.EntityAfter = "";
            log.Operation = "delete";
            log.Date = DateTime.Now.Date;

            var check = await InsertLog.InsertLogAsync(log);

            if (check != "Ok")
            {

                _passenger.InsertOne(passenger);
                return null;

            }

            return user;

        }

    }
}
