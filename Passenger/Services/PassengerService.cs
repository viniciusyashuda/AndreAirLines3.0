using System.Collections.Generic;
using Model;
using MongoDB.Driver;
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

        public Passenger Create(Passenger passenger)
        {

            var passengerFound = GetCpf(passenger.Cpf);

            if (passengerFound == null)
            {

                _passenger.InsertOne(passenger);
                return passenger;

            }

            return null;

        }

        public void Update(string id, Passenger passenger_updated) =>
            _passenger.ReplaceOne(passenger => passenger.Id == id, passenger_updated);

        public void Remove(Passenger passengerToRemove) =>
            _passenger.DeleteOne(passenger => passenger.Id == passengerToRemove.Id);

        public void Remove(string id) =>
            _passenger.DeleteOne(passenger => passenger.Id == id);

    }
}
