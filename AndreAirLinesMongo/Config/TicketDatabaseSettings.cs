namespace TicketMicroService.Config
{
    public class TicketDatabaseSettings:ITicketDatabaseSettings
    {

        public string TicketCollectionName { get; set; } = "Ticket";

        public string ConnectionString { get; set; } = "mongodb://localhost:27017";

        public string DatabaseName { get; set; } = "db_ticket";

    }
}
