using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ModelLibrary;
using Newtonsoft.Json;

namespace ConsumeRest2020
{
    public static class ConsumeGuest
    {
        private const string URI = "http://localhost:50463/api/guest";
        public static async Task<List<Guest>> GetGuestsAsync()
        {
            List<Guest> gæster = new List<Guest>();
            using (HttpClient client = new HttpClient())
            {
                string jsonString = await client.GetStringAsync(URI);

                gæster = JsonConvert.DeserializeObject<List<Guest>>(jsonString);
            }

            return gæster;
        }

        public static async Task<Guest> GetOneGæstAsync(int id)
        {
            Guest gæst = new Guest();
            using (HttpClient client = new HttpClient())
            {
                string stringAsync = await client.GetStringAsync(URI + "/" + id);
                gæst = JsonConvert.DeserializeObject<Guest>(stringAsync);
            }

            return gæst;
        }

        public static async Task<bool> DeleteAsync(int id)
        {
            bool ok = false;
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage deleteAsync = await client.DeleteAsync(URI + "/" + id);
                if (deleteAsync.IsSuccessStatusCode)
                {
                    string jsonStr = deleteAsync.Content.ReadAsStringAsync().Result;
                    ok = JsonConvert.DeserializeObject<bool>(jsonStr);
                }
                else
                {
                    ok = false;
                }

            }

            return ok;
        }

        public static async Task<bool> PostAsync(Guest guest)
        {
            bool ok = false;
            using (HttpClient client = new HttpClient())
            {

                string jsonString = JsonConvert.SerializeObject(guest);

                StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");

                HttpResponseMessage postAsync = await client.PostAsync(URI, content);
               

                if (postAsync.IsSuccessStatusCode)
                {
                    string jsonStr = postAsync.Content.ReadAsStringAsync().Result;
                    ok = JsonConvert.DeserializeObject<bool>(jsonStr);
                }
                else
                {
                    ok = false;
                }

            }

            return ok;
        }

        public static async Task<bool> PutAsync(int id, Guest guest)
        {
            bool ok = false;
            using (HttpClient client = new HttpClient())
            {

                string jsonString = JsonConvert.SerializeObject(guest);

                StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");

                HttpResponseMessage putAsync = await client.PutAsync(URI + "/" + id, content);
                

                if (putAsync.IsSuccessStatusCode)
                {
                    string jsonStr = putAsync.Content.ReadAsStringAsync().Result;
                    ok = JsonConvert.DeserializeObject<bool>(jsonStr);
                }
                else
                {
                    ok = false;
                }

            }

            return ok;
        }
    }
}
