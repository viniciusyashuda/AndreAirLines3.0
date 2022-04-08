using System;
using System.Net.Http;
using System.Threading.Tasks;
using Model;
using Newtonsoft.Json;

namespace BasePriceMicroService.Services
{
    public class SearchAirport
    {

        public static async Task<Airport> FindAirportAsync(string id)
        {

            using var client = new HttpClient();


            try
            {
                HttpResponseMessage response = await client.GetAsync("https://localhost:44393/api/Airport/" + id);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();


                var airport = JsonConvert.DeserializeObject<Airport>(responseBody);

                //var aircraftQuery =
                //    (from aircraft in aircrafts
                //     where aircraft.Id == id
                //     select aircraft).FirstOrDefault();

                return airport;


            }
            catch (Exception exception)
            {
                throw new Exception("Exception: " + exception.Message);
            }

        }

    }
}
