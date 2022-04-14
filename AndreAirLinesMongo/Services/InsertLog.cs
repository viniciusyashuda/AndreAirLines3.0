using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Model;
using Newtonsoft.Json;

namespace TicketMicroService.Services
{
    public class InsertLog
    {

        public static async Task<string> InsertLogAsync(Log log)
        {

            HttpClient client = new HttpClient();

            try
            {

                var json = JsonConvert.SerializeObject(log);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var result = await client.PostAsync("https://localhost:44301/api/Logs", content);
                result.EnsureSuccessStatusCode();
                if (result.IsSuccessStatusCode)
                {

                    return "Ok";

                }

                else
                {

                    return "notOk";

                }

            }
            catch
            {
                return "notOk";
            }

        }

    }
}
