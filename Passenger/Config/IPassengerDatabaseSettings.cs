namespace PassengerMicroService.Config
{
    public interface IPassengerDatabaseSettings
    {

        string PassengerCollectionName { get; set; }

        string ConnectionString { get; set; }

        string DatabaseName { get; set; }

    }
}
