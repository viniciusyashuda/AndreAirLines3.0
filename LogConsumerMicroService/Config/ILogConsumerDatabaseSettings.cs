namespace LogConsumerMicroService.Config
{
    public interface ILogConsumerDatabaseSettings
    {

        string LogCollectionName { get; set; }

        string ConnectionString { get; set; }

        string DatabaseName { get; set; }

    }
}
