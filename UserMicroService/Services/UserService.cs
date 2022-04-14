using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Model;
using MongoDB.Driver;
using Newtonsoft.Json;
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

        public async Task<User> Create(User user)
        {

            var userFound = Get(user.UserLogin);

            if(userFound.Role != "Admin")
            {

                return null;

            }

            if(userFound == null)
            {

                return null;

            }


            if(user.Login == null)
            {

                return null;

            }

            userFound = Get(user.Login);

            if (userFound != null)
            {

                return null;

            }


            _user.InsertOne(user);

            Log log = new();
            log.User = user;
            log.EntityBefore = "";
            log.EntityAfter = JsonConvert.SerializeObject(user);
            log.Operation = "create";
            log.Date = DateTime.Now.Date;

            var check = await InsertLog.InsertLogAsync(log);

            if (check != "Ok")
            {

                _user.DeleteOne(userIn => userIn.Id == user.Id);
                return null;

            }

            return user;

        }

        public async Task<User> Update(string login, User user_updated)
        {

            if (user_updated.UserLogin == null)
            {

                return null;

            }

            var user = Get(user_updated.UserLogin);

            if (user == null)
            {

                return null;

            }
            if (user.Role != "Admin")
            {

                return null;

            }

            var userBefore = Get(login);

            _user.ReplaceOne(user => user.Login == login, user_updated);

            Log log = new();
            log.User = user;
            log.EntityBefore = JsonConvert.SerializeObject(userBefore);
            log.EntityAfter = JsonConvert.SerializeObject(user_updated);
            log.Operation = "update";
            log.Date = DateTime.Now.Date;

            var check = await InsertLog.InsertLogAsync(log);

            if (check != "Ok")
            {

                _user.ReplaceOne(userIn => userIn.Id == user_updated.Id, userBefore);
                return null;

            }

            return user;
        }

        public void Remove(User userToRemove) =>
            _user.DeleteOne(user => user.Login == userToRemove.Login);

        public async Task<User> Remove(string login, User user)
        {

            if (user.UserLogin == null)
            {

                return null;

            }

            user = Get(user.UserLogin);

            if (user == null)
            {

                return null;

            }
            if (user.Role != "Admin")
            {

                return null;

            }

            var userBefore = Get(login);

            _user.DeleteOne(user =>  user.Login == login);

            Log log = new();
            log.User = user;
            log.EntityBefore = JsonConvert.SerializeObject(userBefore);
            log.EntityAfter = "";
            log.Operation = "delete";
            log.Date = DateTime.Now.Date;

            var check = await InsertLog.InsertLogAsync(log);

            if (check != "Ok")
            {

                _user.InsertOne(userBefore);
                return null;

            }

            return user;
        }

    }
}
