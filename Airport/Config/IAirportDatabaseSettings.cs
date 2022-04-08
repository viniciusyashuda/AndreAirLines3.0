namespace AirportMicroService.Config
{
    public interface IAirportDatabaseSettings
    {

        string AirportCollectionName { get; set; }

        string ConnectionString { get; set; }

        string DatabaseName { get; set; }

    }
}
