namespace LogConsumerMicroService.Config
{
    public class LogConsumerDatabaseSettings : ILogConsumerDatabaseSettings
    {
        public string LogCollectionName { get; set; }

        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; }

    }
}
