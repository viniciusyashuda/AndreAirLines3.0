﻿using System;
using System.Net.Http;
using System.Threading.Tasks;
using Model;
using Newtonsoft.Json;

namespace BasePriceMicroService.Services
{
    public class SearchUser
    {

        public static async Task<User> FindUserAsync(string login)
        {

            using var client = new HttpClient();


            try
            {
                HttpResponseMessage response = await client.GetAsync("https://localhost:44356/api/User/" + login);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();


                var user = JsonConvert.DeserializeObject<User>(responseBody);

                return user;


            }
            catch (Exception exception)
            {
                throw new Exception("Exception: " + exception.Message);
            }

        }

    }
}
