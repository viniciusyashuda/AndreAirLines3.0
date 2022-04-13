using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FlightMicroService.Config;
using Model;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace FlightMicroService.Services
{
    public class FlightService
    {

        public readonly IMongoCollection<Flight> _flight;

        public FlightService(IFlightDatabaseSettings settings)
        {

            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _flight = database.GetCollection<Flight>(settings.FlightCollectionName);

        }


        public List<Flight> Get() =>
            _flight.Find(flight => true).ToList();

        public Flight Get(string id) =>
            _flight.Find<Flight>(flight => flight.Id == id).FirstOrDefault();

        public async Task <Flight> Create(Flight flight)
        {

            if (flight.UserLogin == null)
            {

                return null;

            }

            var user = await SearchUser.FindUserAsync(flight.UserLogin);

            if (user == null)
            {

                return null;

            }
            if (user.Role != "Admin" && user.Role != "User")
            {

                return null;

            }

            var aircraft = await SearchAircraft.FindAircraftAsync(flight.Aircraft.Id);

            if (aircraft == null)
            {

                return null;

            }

            var origin = await SearchAirport.FindAirportAsync(flight.Origin.Id);

            if (origin == null)
            {

                return null;

            }

            var destination = await SearchAirport.FindAirportAsync(flight.Destination.Id);

            if (destination == null)
            {

                return null;

            }

            if(destination.Id.Equals(origin.Id))
            {

                return null;

            }


            flight.Origin = origin;
            flight.Destination = destination;
            flight.Aircraft = aircraft;

            _flight.InsertOne(flight);

            Log log = new();
            log.User = user;
            log.EntityBefore = "";
            log.EntityAfter = JsonConvert.SerializeObject(flight);
            log.Operation = "create";
            log.Date = DateTime.Now.Date;

            var check = await InsertLog.InsertLogAsync(log);

            if (check != "Ok")
            {

                _flight.DeleteOne(flightIn => flightIn.Id == flight.Id);
                return null;

            }

            return flight;

        }

        public async Task <Flight> Update(string id, Flight flight_updated)
        {

            if (flight_updated.UserLogin == null)
            {

                return null;

            }

            var user = await SearchUser.FindUserAsync(flight_updated.UserLogin);

            if (user == null)
            {

                return null;

            }
            if (user.Role != "Admin" && user.Role != "User")
            {

                return null;

            }

            var aircraft = await SearchAircraft.FindAircraftAsync(flight_updated.Aircraft.Id);

            if (aircraft == null)
            {

                return null;

            }

            var origin = await SearchAirport.FindAirportAsync(flight_updated.Origin.Id);

            if (origin == null)
            {

                return null;

            }

            var destination = await SearchAirport.FindAirportAsync(flight_updated.Destination.Id);

            if (destination == null)
            {

                return null;

            }

            if (destination.Id.Equals(origin.Id))
            {

                return null;

            }

            var flight = Get(id);

            _flight.ReplaceOne(flightIn => flightIn.Id == id, flight_updated);

            Log log = new();
            log.User = user;
            log.EntityBefore = JsonConvert.SerializeObject(flight);
            log.EntityAfter = JsonConvert.SerializeObject(flight_updated);
            log.Operation = "update";
            log.Date = DateTime.Now.Date;

            var check = await InsertLog.InsertLogAsync(log);

            if (check != "Ok")
            {

                _flight.ReplaceOne(flightIn => flightIn.Id == flight_updated.Id, flight);
                return null;

            }

            return flight_updated;

        }

        public void Remove(Flight flightToRemove) =>
            _flight.DeleteOne(flight => flight.Id == flightToRemove.Id);

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

            var flight = Get(id);

            _flight.DeleteOne(flightIn => flightIn.Id == id);

            Log log = new();
            log.User = user;
            log.EntityBefore = JsonConvert.SerializeObject(flight);
            log.EntityAfter = "";
            log.Operation = "delete";
            log.Date = DateTime.Now.Date;

            var check = await InsertLog.InsertLogAsync(log);

            if (check != "Ok")
            {

                _flight.InsertOne(flight);
                return null;

            }

            return user;

        }

    }
}
