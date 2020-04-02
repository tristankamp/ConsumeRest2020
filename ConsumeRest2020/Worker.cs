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
    public class Worker
    {
        private const string URI = "http://localhost:50463";
        public async Task Start()
        {
            //List<Hotel> hoteller = GetHotels();
            //hoteller = GetHotels();
            //foreach (Hotel hotel in hoteller)
            //{
            //    Console.WriteLine("Hotel ::"+hotel);
            //}
            //Console.WriteLine("Hent hotel no 5 :" + GetOneHotel(5) );
            //Console.WriteLine("Delete hotel nr 100");
            //Console.WriteLine("Hotel nr 100 deleted: " + Delete(100));

            Hotel postHotel = new Hotel(101, "kage", "kagemand 13");
            Task<bool> ok = PostAsync(postHotel);
            Console.WriteLine("Har post hotel nr 101" + ok);

            //Hotel newOpdateretHotel = new Hotel(101, "zoomOpdateretHotel", "Kagemandsstræde 123");
            //bool ok = Put(101, newOpdateretHotel);
            //Console.WriteLine("Har oprettet hotel nr 101" + ok);

            //List<Hotel> hoteller = await GetHotelsAsync();
            //foreach (Hotel hotel in hoteller)
            //{
            //    Console.WriteLine("Hotel ::" + hotel);
            //}

            //List<Guest> gæster = await ConsumeGuest.GetGuestsAsync();
            //foreach (Guest gæst in gæster)
            //{
            //    Console.WriteLine("Gæst ::" + gæst);
            //}
        }

        public async Task<List<Hotel>> GetHotelsAsync()
        {
            List<Hotel> hoteller = new List<Hotel>();
            using (HttpClient client = new HttpClient())
            {
                string jsonString = await client.GetStringAsync(URI + "/api/hotels");
                
                hoteller = JsonConvert.DeserializeObject<List<Hotel>>(jsonString);
            }

            return hoteller;
        }

        private async Task<Hotel>GetOneHotelAsync(int id)
        {
            Hotel hotel = new Hotel();
            using (HttpClient client = new HttpClient())
            {
                string stringAsync = await client.GetStringAsync(URI + "/" + id);
                hotel = JsonConvert.DeserializeObject<Hotel>(stringAsync);
            }

            return hotel;
        }

        public async Task<bool> DeleteAsync(int id)
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

        public async Task<bool> PostAsync(Hotel hotel)
        {
            bool ok = false;
            using (HttpClient client = new HttpClient())
            {

                string jsonString = JsonConvert.SerializeObject(hotel);

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

        public async Task<bool> PutAsync(int id, Hotel hotel)
        {
            bool ok = false;
            using (HttpClient client = new HttpClient())
            {

                string jsonString = JsonConvert.SerializeObject(hotel);

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
