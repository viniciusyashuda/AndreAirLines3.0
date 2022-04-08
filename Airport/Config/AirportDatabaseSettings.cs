namespace AirportMicroService.Config
{
    public class AirportDatabaseSettings:IAirportDatabaseSettings
    {

        public string AirportCollectionName { get; set; }

        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; }

    }
}
