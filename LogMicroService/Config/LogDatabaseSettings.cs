namespace LogMicroService.Config
{
    public class LogDatabaseSettings : ILogDatabaseSettings
    {

        public string LogCollectionName { get; set; }

        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; }

    }
}
