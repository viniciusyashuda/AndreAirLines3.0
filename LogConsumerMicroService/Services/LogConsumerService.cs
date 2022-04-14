using System.Collections.Generic;
using System.Threading.Tasks;
using LogConsumerMicroService.Config;
using Model;
using MongoDB.Driver;

namespace LogConsumerMicroService.Services
{
    public class LogConsumerService
    {

        private readonly IMongoCollection<Log> _logconsumer;

        public LogConsumerService(ILogConsumerDatabaseSettings settings)
        {

            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _logconsumer = database.GetCollection<Log>(settings.LogCollectionName);

        }

        public async Task<List<Log>> Get() =>
            await _logconsumer.Find(log => true).ToListAsync();

        public async Task<Log> Get(string id) =>
            await _logconsumer.Find(log => log.Id == id).FirstOrDefaultAsync();

        public Log Create(Log log)
        {

            _logconsumer.InsertOne(log);
            return log;

        }


    }
}
