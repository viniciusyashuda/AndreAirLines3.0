namespace FlightMicroService.Config
{
    public interface IFlightDatabaseSettings
    {

        string FlightCollectionName { get; set; }

        string ConnectionString { get; set; }

        string DatabaseName { get; set; }


    }
}
