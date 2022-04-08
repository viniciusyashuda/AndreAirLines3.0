using System.Collections.Generic;
using System.Threading.Tasks;
using FlightMicroService.Config;
using Model;
using MongoDB.Driver;

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
            return flight;

        }

        public void Update(string id, Flight flight_updated) =>
            _flight.ReplaceOne(flight => flight.Id == id, flight_updated);

        public void Remove(Flight flightToRemove) =>
            _flight.DeleteOne(flight => flight.Id == flightToRemove.Id);

        public void Remove(string id) =>
            _flight.DeleteOne(flight => flight.Id == id);

    }
}
