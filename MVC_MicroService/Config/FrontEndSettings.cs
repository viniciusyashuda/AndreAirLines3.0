namespace MVC_MicroService.Config
{
    public class FrontEndSettings:IFrontEndSettings
    {
        public string AirportCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }

    }
}
