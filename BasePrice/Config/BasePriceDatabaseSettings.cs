namespace BasePriceMicroService.Config
{
    public class BasePriceDatabaseSettings:IBasePriceDatabaseSettings
    {
        public string BasePriceCollectionName { get; set; }

        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; }

    }
}
