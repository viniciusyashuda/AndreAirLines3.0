using System.Collections.Generic;
using AirportMicroService.Config;
using Model;
using MongoDB.Driver;

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


        public Airport Create(Airport airport)
        {

            var airportFound = GetIATA_Code(airport.IATA_Code);

            if (airportFound == null)
            {

                _airport.InsertOne(airport);
                return airport;

            }

            return null;
          

        }

        public void Update(string id, Airport airport_updated) =>
            _airport.ReplaceOne(aircraft => aircraft.Id == id, airport_updated);

        public void Remove(Airport airportToRemove) =>
            _airport.DeleteOne(airport => airport.Id == airportToRemove.Id);

        public void Remove(string id) =>
            _airport.DeleteOne(airport => airport.Id == id);

    }
}
