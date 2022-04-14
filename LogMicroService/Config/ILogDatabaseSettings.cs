namespace LogMicroService.Config
{
    public interface ILogDatabaseSettings
    {

        string LogCollectionName { get; set; }

        string ConnectionString { get; set; }

        string DatabaseName { get; set; }

    }
}
