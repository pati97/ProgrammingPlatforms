using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WorldWeather
{
    public class WeatherConnection
    {
        private static string apiKey = "76cf4323d742a8bce713d0607b982631";
        private static string apiBaseUrl = "http://api.openweathermap.org/data/2.5/weather";

        public async static Task<string> LoadWeatherAsync(string city)
        {
            if (CheckForInternetConnection() == false)
            {
                MessageBox.Show("Cannot connect with internet !");
                Application.Current.Shutdown();
            }

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

        private static bool CheckForInternetConnection()
        {
            try
            {
                using (var mobileClient = new WebClient())
                using (var webConnection = mobileClient.OpenRead("http://www.google.com"))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
