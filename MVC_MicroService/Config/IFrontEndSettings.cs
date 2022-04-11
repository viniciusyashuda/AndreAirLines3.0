namespace MVC_MicroService.Config
{
    public interface IFrontEndSettings
    {
        string AirportCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }

    }
}
