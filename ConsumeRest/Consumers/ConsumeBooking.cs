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
    public class ConsumeBooking
    {
        private const string URI = "http://localhost:56897/api/Bookings";
        public static async Task<List<Booking>> GetAllBookingsAsync()
        {
            List<Booking> bookings = null;

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage resp = await client.GetAsync(URI);
                if (resp.IsSuccessStatusCode)
                {
                    string result = await resp.Content.ReadAsStringAsync();
                    bookings = JsonConvert.DeserializeObject<List<Booking>>(result);
                }
                else
                {
                    throw new HttpRequestException(resp.StatusCode.ToString());
                }
            }

            return bookings;
        }

        public static async Task<Booking> GetOneBookingAsync(int id)
        {
            Booking booking = null;
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage resp = await client.GetAsync(URI + "/" + id);
                if (resp.IsSuccessStatusCode)
                {
                    string result = await resp.Content.ReadAsStringAsync();
                    booking = JsonConvert.DeserializeObject<Booking>(result);
                }
                else
                {
                    throw new HttpRequestException(resp.StatusCode.ToString());
                }
            }

            return booking;
        }

        public static async Task<bool> PostBooking(Booking booking)
        {
            bool ok = false;

            using (HttpClient client = new HttpClient())
            {
                string jsonStr = JsonConvert.SerializeObject(booking);
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

        public static async Task<bool> PutBooking(Booking booking, int id)
        {
            bool ok = false;

            using (HttpClient client = new HttpClient())
            {
                string jsonStr = JsonConvert.SerializeObject(booking);
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

        public static async Task<bool> DeleteBooking(int id)
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
