namespace AircraftMicroService.Config
{
    public class AircraftDatabaseSettings : IAircraftDatabaseSettings
    {

        public string AircraftCollectionName { get; set; }

        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; }

    }
}
