namespace TicketMicroService.Config
{
    public class TicketDatabaseSettings:ITicketDatabaseSettings
    {

        public string TicketCollectionName { get; set; }

        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; }

    }
}
