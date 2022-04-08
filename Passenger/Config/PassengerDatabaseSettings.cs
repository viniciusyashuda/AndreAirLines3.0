namespace PassengerMicroService.Config
{
    public class PassengerDatabaseSettings : IPassengerDatabaseSettings
    {

        public string PassengerCollectionName { get; set; }

        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; }


    }
}
