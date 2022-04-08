using System;
using System.Net.Http;
using System.Threading.Tasks;
using Model;
using Newtonsoft.Json;

namespace TicketMicroService.Services
{
    public class SearchFlight
    {

        public static async Task<Flight> FindFlightAsync(string id)
        {

            using var client = new HttpClient();


            try
            {
                HttpResponseMessage response = await client.GetAsync("https://localhost:44318/api/Flight/" + id);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();


                var flight = JsonConvert.DeserializeObject<Flight>(responseBody);

                //var aircraftQuery =
                //    (from aircraft in aircrafts
                //     where aircraft.Id == id
                //     select aircraft).FirstOrDefault();

                return flight;


            }
            catch (Exception exception)
            {
                throw new Exception("Exception: " + exception.Message);
            }

        }

    }
}
