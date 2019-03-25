using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    class WeatherConnection
    {
        public static async Task<string> LoadDataAsync(string city)
        {
            string url = "http://api.apixu.com/v1/current.xml?key=78ae3fdd414d4f168ed130602191703&q=" + city;
            //string url = "https://api.openweathermap.org/data/2.5/weather?q=" + city + "&apikey=1b6714e500f0cdd864a8b49ec6ac5e45&mode=xml";
            Task<string> result;
            using (HttpClient client = new HttpClient())
            using (HttpResponseMessage response = await client.GetAsync(url))
            using (HttpContent content = response.Content)
            {
                result = content.ReadAsStringAsync();
            }
            return await result;
        }
    }
}
