namespace AircraftMicroService.Config
{
    public interface IAircraftDatabaseSettings
    {

        string AircraftCollectionName { get; set; }

        string ConnectionString { get; set; }

        string DatabaseName { get; set; }

    }
}
