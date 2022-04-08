using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Model;
using Newtonsoft.Json;

namespace FlightMicroService.Services
{
    public class SearchAircraft
    {

        public static async Task<Aircraft> FindAircraftAsync(string id)
        {

            using var client = new HttpClient();


            try
            {
                HttpResponseMessage response = await client.GetAsync("https://localhost:44311/api/Aircraft/" + id);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();


                var aircraft = JsonConvert.DeserializeObject<Aircraft>(responseBody);

                //var aircraftQuery =
                //    (from aircraft in aircrafts
                //     where aircraft.Id == id
                //     select aircraft).FirstOrDefault();

                return aircraft;


            }
            catch (Exception exception)
            {
                throw new Exception("Exception: " + exception.Message);
            }

        }

    }
}
