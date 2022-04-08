namespace BasePriceMicroService.Config
{
    public interface IBasePriceDatabaseSettings
    {

        string BasePriceCollectionName { get; set; }

        string ConnectionString { get; set; }

        string DatabaseName { get; set; }

    }
}
