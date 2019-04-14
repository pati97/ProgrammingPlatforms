using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WorldWeather
{
    public class WeatherConnection
    {
        private static string apiKey = "76cf4323d742a8bce713d0607b982631";
        private static string apiBaseUrl = "http://api.openweathermap.org/data/2.5/weather";

        public async static Task<string> LoadWeatherAsync(string city)
        {
            string apiCall = apiBaseUrl + "?q=" + city + "&mode=xml" + "&apikey=" + apiKey;
  
            Task<string> result;
            using (HttpClient client = new HttpClient())
            using (HttpResponseMessage response = await client.GetAsync(apiCall))
            using (HttpContent content = response.Content)
            {
                result = content.ReadAsStringAsync();
            }
            return await result;
        }
    }
}
