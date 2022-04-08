using System;
using System.Net.Http;
using System.Threading.Tasks;
using Model;
using Newtonsoft.Json;

namespace TicketMicroService.Services
{
    public class SearchBasePrice
    {
        public static async Task<BasePrice> FindBasePriceAsync(string id)
        {

            using var client = new HttpClient();


            try
            {
                HttpResponseMessage response = await client.GetAsync("https://localhost:44349/api/BasePrice/" + id);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();


                var base_price = JsonConvert.DeserializeObject<BasePrice>(responseBody);

                //var aircraftQuery =
                //    (from aircraft in aircrafts
                //     where aircraft.Id == id
                //     select aircraft).FirstOrDefault();

                return base_price;


            }
            catch (Exception exception)
            {
                throw new Exception("Exception: " + exception.Message);
            }

        }
    }
}
