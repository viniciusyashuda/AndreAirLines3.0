using System;
using System.Net.Http;
using System.Threading.Tasks;
using Model;
using Newtonsoft.Json;

namespace TicketMicroService.Services
{
    public class SearchPassenger
    {

        public static async Task<Passenger> FindPassengerAsync(string id)
        {

            using var client = new HttpClient();


            try
            {
                HttpResponseMessage response = await client.GetAsync("https://localhost:44334/api/Passenger/" + id);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();


                var passenger = JsonConvert.DeserializeObject<Passenger>(responseBody);

                //var aircraftQuery =
                //    (from aircraft in aircrafts
                //     where aircraft.Id == id
                //     select aircraft).FirstOrDefault();

                return passenger;


            }
            catch (Exception exception)
            {
                throw new Exception("Exception: " + exception.Message);
            }

        }

    }
}
