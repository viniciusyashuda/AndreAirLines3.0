using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Model;
using Newtonsoft.Json;

namespace RobotSQL.Service
{
    public class Dapper_vs_EntityFramework
    {

        public void PerformanceTest()
        {

            DateTime end = DateTime.Now, start = DateTime.Now;

            for (int id = 1; id <= 100; id++)
            {

                EntityFrameworkPerformanceAsync(id).Wait();


                if(id == 1)
                {
                    start = DateTime.Now;
                }
                
                if(id == 100)
                {
                    end = DateTime.Now;
                }

            }

            var timeEntityFramework = end - start;

            Console.WriteLine("Entity Framework time: " + timeEntityFramework);

            Console.ReadKey();
            Console.Clear();

            for (int id = 1; id <= 100; id++)
            {

                DapperPerformanceAsync(id).Wait();

                if (id == 1)
                {
                    start = DateTime.Now;
                }

                if (id == 100)
                {
                    end = DateTime.Now;
                }

            }

            var timeDapper = end - start;

            Console.WriteLine("Dapper time: " + timeDapper);

            Console.ReadKey();
            Console.Clear();

            Console.WriteLine("Entity Framework time: " + timeEntityFramework);
            Console.WriteLine("Dapper time: " + timeDapper);
            
            if(timeEntityFramework < timeDapper)
                Console.WriteLine("Entity Framework had a better perfomance!");
            else if(timeEntityFramework > timeDapper)
                Console.WriteLine("Dapper had a better perfomance!");
            else if(timeEntityFramework == timeDapper)
                Console.WriteLine("Both had the same performance!");


        }

        public static async Task EntityFrameworkPerformanceAsync(int id)
        {

            using var client = new HttpClient();


            try
            {
                HttpResponseMessage response = await client.GetAsync("https://localhost:44319/api/AirportSQL/" + id);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();


                var airport = JsonConvert.DeserializeObject<AirportSQL>(responseBody);

                    Console.WriteLine(airport);
                    Console.WriteLine(DateTime.Now);
                    Console.WriteLine("\n");

            }
            catch (Exception exception)
            {
                throw new Exception("Exception: " + exception.Message);
            }


        }

        public static async Task DapperPerformanceAsync(int id)
        {

            using var client = new HttpClient();


            try
            {
                HttpResponseMessage response = await client.GetAsync("https://localhost:44309/api/AirportSQL/" + id);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();


                var airport = JsonConvert.DeserializeObject<AirportSQL>(responseBody);
 
                    Console.WriteLine(airport);
                    Console.WriteLine(DateTime.Now);


            }
            catch (Exception exception)
            {
                throw new Exception("Exception: " + exception.Message);
            }


        }

    }
}
