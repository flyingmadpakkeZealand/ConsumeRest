using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ModelLibrary;
using Newtonsoft.Json;

namespace ConsumeRest.Consumers
{
    public static class ConsumeHotel
    {
        private const string URI = "http://localhost:56897/api/Hotels";
        public static async Task<List<Hotel>> GetAllHotelsAsync()
        {
            List<Hotel> hotels = null;

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage resp = await client.GetAsync(URI);
                if (resp.IsSuccessStatusCode)
                {
                    string result = await resp.Content.ReadAsStringAsync();
                    hotels = JsonConvert.DeserializeObject<List<Hotel>>(result);
                }
                else
                {
                    throw new HttpRequestException(resp.StatusCode.ToString());
                }
            }

            return hotels;
        }

        public static async Task<Hotel> GetOneHotelAsync(int id)
        {
            Hotel hotel = null;
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage resp = await client.GetAsync(URI + "/" + id);
                if (resp.IsSuccessStatusCode)
                {
                    string result = await resp.Content.ReadAsStringAsync();
                    hotel = JsonConvert.DeserializeObject<Hotel>(result);
                }
                else
                {
                    throw new HttpRequestException(resp.StatusCode.ToString());
                }
            }

            return hotel;
        }

        public static async Task<bool> PostHotel(Hotel hotel)
        {
            bool ok = false;

            using (HttpClient client = new HttpClient())
            {
                string jsonStr = JsonConvert.SerializeObject(hotel);
                StringContent content = new StringContent(jsonStr, Encoding.UTF8, "application/json");
                HttpResponseMessage resp = await client.PostAsync(URI, content);
                if (resp.IsSuccessStatusCode)
                {
                    string result = await resp.Content.ReadAsStringAsync();
                    ok = JsonConvert.DeserializeObject<bool>(result);
                }
                else
                {
                    throw new HttpRequestException(resp.StatusCode.ToString());
                }
            }

            return ok;
        }

        public static async Task<bool> PutHotel(Hotel hotel, int id)
        {
            bool ok = false;

            using (HttpClient client = new HttpClient())
            {
                string jsonStr = JsonConvert.SerializeObject(hotel);
                StringContent content = new StringContent(jsonStr, Encoding.UTF8, "application/json");
                HttpResponseMessage resp = await client.PutAsync(URI + "/" + id, content);
                if (resp.IsSuccessStatusCode)
                {
                    string result = await resp.Content.ReadAsStringAsync();
                    ok = JsonConvert.DeserializeObject<bool>(result);
                }
                else
                {
                    throw new HttpRequestException(resp.StatusCode.ToString());
                }
            }

            return ok;
        }

        public static async Task<bool> DeleteHotel(int id)
        {
            bool ok = false;

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage resp = await client.DeleteAsync(URI + "/" + id);
                if (resp.IsSuccessStatusCode)
                {
                    string result = await resp.Content.ReadAsStringAsync();
                    ok = JsonConvert.DeserializeObject<bool>(result);
                }
                else
                {
                    throw new HttpRequestException(resp.StatusCode.ToString());
                }
            }

            return ok;
        }
    }
}
