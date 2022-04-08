using System;
using System.Net.Http;
using System.Threading.Tasks;
using Model;
using Newtonsoft.Json;

namespace Services
{
    public class ViaCep
    {

        static readonly HttpClient client = new HttpClient();
        public static async Task<Address> GetAddressViaCep(string postalCode)
        {

            try
            {

                HttpResponseMessage response = await client.GetAsync("https://viacep.com.br/ws/" + postalCode + "/json/");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                var address = JsonConvert.DeserializeObject<Address>(responseBody);
                return address;

            }
            catch (HttpRequestException exception)
            {

                throw new HttpRequestException(exception.Message);

            }
        
        }

    }
}
