using System.Collections.Generic;
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

        public List<Log> Get() =>
            _log.Find(log => true).ToList();

        public Log Get(string id) =>
            _log.Find(log => log.Id == id).FirstOrDefault();

        public Log Create(Log log)
        {
            var logFound = Get(log.Id);

            if (logFound == null)
            {

                _log.InsertOne(log);
                return log;

            }

            return null;

        }

        public void Update(string id, Log log_updated) =>
            _log.ReplaceOne(log => log.Id == id, log_updated);

        public void Remove(Log logToRemove) =>
            _log.DeleteOne(log => log.Id == logToRemove.Id);

        public void Remove(string id) =>
            _log.DeleteOne(log => log.Id == id);

    }

}
