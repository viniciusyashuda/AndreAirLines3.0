using System.Collections.Generic;
using System.Threading.Tasks;
using BasePriceMicroService.Config;
using Model;
using MongoDB.Driver;

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

        public async Task<BasePrice> Create (BasePrice base_price)
        {

            var base_price_found = Get(base_price.Id);

            if(base_price_found != null)
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

            if(origin.Id.Equals(destination.Id))
            {

                return null;

            }


            base_price.Origin = origin;
            base_price.Destination = destination;

            _baseprice.InsertOne(base_price);
            return base_price;

        }

        public void Update(string id, BasePrice base_price_updated) =>
            _baseprice.ReplaceOne(base_price => base_price.Id == id, base_price_updated);

        public void Remove(BasePrice base_priceToRemove) =>
            _baseprice.DeleteOne(base_price => base_price.Id == base_priceToRemove.Id);

        public void Remove(string id) =>
            _baseprice.DeleteOne(base_price => base_price.Id == id);


    }
}
