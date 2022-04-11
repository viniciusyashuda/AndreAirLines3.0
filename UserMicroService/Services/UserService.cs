using System.Collections.Generic;
using System.Threading.Tasks;
using Model;
using MongoDB.Driver;
using UserMicroService.Config;

namespace UserMicroService.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _user;

        public UserService(IUserDatabaseSettings settings)
        {

            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _user = database.GetCollection<User>(settings.UserCollectionName);

        }

        public List<User> Get() =>
            _user.Find(user => true).ToList();

        public User Get(string login) =>
            _user.Find(user => user.Login == login).FirstOrDefault();

        public User Create(User user)
        {

            _user.InsertOne(user);
            return user;

        }

        public void Update(string login, User user_updated) =>
            _user.ReplaceOne(user => user.Login == login, user_updated);

        public void Remove(User userToRemove) =>
            _user.DeleteOne(user => user.Login == userToRemove.Login);

        public void Remove(string login) =>
            _user.DeleteOne(user =>  user.Login == login);

    }
}
