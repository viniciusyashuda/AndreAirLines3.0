using System.Collections.Generic;
using AircraftMicroService.Config;
using Model;
using MongoDB.Driver;

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

        public Aircraft Create(Aircraft aircraft)
        {

            var aircraftFound = GetRegistration(aircraft.Registration);

            if(aircraftFound == null)
            {

                _aircraft.InsertOne(aircraft);
                return aircraft;

            }

            return null;

        }

        public void Update(string id, Aircraft aircraft_updated) =>
            _aircraft.ReplaceOne(aircraft => aircraft.Id == aircraft_updated.Id, aircraft_updated);

        public void Remove(Aircraft aircraftToRemove) =>
            _aircraft.DeleteOne(aircraft => aircraft.Id == aircraftToRemove.Id);

        public void Remove(string id) =>
            _aircraft.DeleteOne(aircraft => aircraft.Id == id);

    }
}
