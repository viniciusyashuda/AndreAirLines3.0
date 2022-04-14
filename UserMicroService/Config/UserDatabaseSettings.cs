namespace UserMicroService.Config
{
    public class UserDatabaseSettings : IUserDatabaseSettings
    {

        public string UserCollectionName { get; set; }

        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; }

    }
}
