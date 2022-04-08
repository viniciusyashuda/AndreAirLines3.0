namespace FlightMicroService.Config
{
    public class FlightDatabaseSettings:IFlightDatabaseSettings
    {

        public string FlightCollectionName { get; set; }

        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; }

    }
}
