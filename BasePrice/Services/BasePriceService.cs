using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BasePriceMicroService.Config;
using Model;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace BasePriceMicroService.Services
{
    public class BasePriceService
    {

        private readonly IMongoCollection<BasePrice> _baseprice;

        public BasePriceService(IBasePriceDatabaseSettings settings)
        {

            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _baseprice = database.GetCollection<BasePrice>(settings.BasePriceCollectionName);

        }

        public List<BasePrice> Get() =>
            _baseprice.Find(base_price => true).ToList();

        public BasePrice Get(string id) =>
            _baseprice.Find<BasePrice>(base_price => base_price.Id == id).FirstOrDefault();

        public async Task<BasePrice> Create(BasePrice base_price)
        {

            if (base_price.UserLogin == null)
            {

                return null;

            }

            var user = await SearchUser.FindUserAsync(base_price.UserLogin);

            if (user == null)
            {

                return null;

            }
            if (user.Role != "Admin")
            {

                return null;

            }

            var base_price_found = Get(base_price.Id);

            if (base_price_found != null)
            {

                return null;

            }

            var origin = await SearchAirport.FindAirportAsync(base_price.Origin.Id);

            if (origin == null)
            {

                return null;

            }

            var destination = await SearchAirport.FindAirportAsync(base_price.Destination.Id);

            if (destination == null)
            {

                return null;

            }

            if (origin.Id.Equals(destination.Id))
            {

                return null;

            }


            base_price.Origin = origin;
            base_price.Destination = destination;

            _baseprice.InsertOne(base_price);

            Log log = new();
            log.User = user;
            log.EntityBefore = "";
            log.EntityAfter = JsonConvert.SerializeObject(base_price);
            log.Operation = "create";
            log.Date = DateTime.Now.Date;

            var check = await InsertLog.InsertLogAsync(log);

            if (check != "Ok")
            {

                _baseprice.DeleteOne(basepriceIn => basepriceIn.Id == base_price.Id);
                return null;

            }

            return base_price;

        }

        public async Task<BasePrice> Update(string id, BasePrice base_price_updated)
        {

            if (base_price_updated.UserLogin == null)
            {

                return null;

            }

            var user = await SearchUser.FindUserAsync(base_price_updated.UserLogin);

            if (user == null)
            {

                return null;

            }
            if (user.Role != "Admin")
            {

                return null;

            }

            var base_price = Get(id);

            _baseprice.ReplaceOne(basepriceIn => basepriceIn.Id == id, base_price_updated);

            Log log = new();
            log.User = user;
            log.EntityBefore = JsonConvert.SerializeObject(base_price);
            log.EntityAfter = JsonConvert.SerializeObject(base_price_updated);
            log.Operation = "update";
            log.Date = DateTime.Now.Date;

            var check = await InsertLog.InsertLogAsync(log);

            if (check != "Ok")
            {

                _baseprice.ReplaceOne(basepriceIn => basepriceIn.Id == base_price_updated.Id, base_price);
                return null;

            }

            return base_price_updated;

        }

        public void Remove(BasePrice base_priceToRemove) =>
            _baseprice.DeleteOne(base_price => base_price.Id == base_priceToRemove.Id);

        public async Task<User> Remove(string id, User user)
        {

            if (user.UserLogin == null)
            {

                return null;

            }

            user = await SearchUser.FindUserAsync(user.UserLogin);

            if (user == null)
            {

                return null;

            }
            if (user.Role != "Admin")
            {

                return null;

            }

            var base_price = Get(id);

            _baseprice.DeleteOne(basepriceIn => basepriceIn.Id == id);

            Log log = new();
            log.User = user;
            log.EntityBefore = JsonConvert.SerializeObject(base_price);
            log.EntityAfter = "";
            log.Operation = "delete";
            log.Date = DateTime.Now.Date;

            var check = await InsertLog.InsertLogAsync(log);

            if (check != "Ok")
            {

                _baseprice.InsertOne(base_price);
                return null;

            }

            return user;

        }


    }
}
