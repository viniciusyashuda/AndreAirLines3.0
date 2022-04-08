namespace TicketMicroService.Config
{
    public interface ITicketDatabaseSettings
    {

        string TicketCollectionName { get; set; }

        string ConnectionString { get; set; }

        string DatabaseName { get; set; }

    }
}
