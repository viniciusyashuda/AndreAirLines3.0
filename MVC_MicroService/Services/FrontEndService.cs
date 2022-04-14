using System.Collections.Generic;
using Model;
using MongoDB.Driver;
using MVC_MicroService.Config;

namespace MVC_MicroService.Services
{
    public class FrontEndService
    {

        private readonly IMongoCollection<Airport> _airport;

        public FrontEndService(IFrontEndSettings config)
        {
            var client = new MongoClient(config.ConnectionString);
            var database = client.GetDatabase(config.DatabaseName);
            _airport = database.GetCollection<Airport>(config.AirportCollectionName);
        }

        public List<Airport> Get() =>
            _airport.Find(airport => true).ToList();



    }
}
