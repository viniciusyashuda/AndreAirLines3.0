using System.Collections.Generic;
using System.Threading.Tasks;
using LogMicroService.Config;
using Model;
using MongoDB.Driver;

namespace LogMicroService.Services
{
    public class LogService
    {

        private readonly IMongoCollection<Log> _log;

        public LogService(ILogDatabaseSettings settings)
        {

            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _log = database.GetCollection<Log>(settings.LogCollectionName);

        }

        public async Task<List<Log>> Get() =>
            await _log.Find(log => true).ToListAsync();

        public async Task<Log> Get(string id) =>
            await _log.Find(log => log.Id == id).FirstOrDefaultAsync();

        public async Task<Log> Create(Log log)
        {
            var logFound = await Get(log.Id);

            if (logFound == null)
            {

                _log.InsertOne(log);
                return log;

            }

            return null;

        }

    }

}
