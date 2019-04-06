using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace WorldWeather
{
    class ForecastWeather
    {
        public class Location
        {
            public string name { get; set; }
            public string region { get; set; }
            public string country { get; set; }
            public double lat { get; set; }
            public double lon { get; set; }
            public string tz_id { get; set; }
            public int localtime_epoch { get; set; }
            public string localtime { get; set; }
        }

        public class Condition
        {
            public string text { get; set; }
            public string icon { get; set; }
            public int code { get; set; }
        }

        public class Current
        {
            public int last_updated_epoch { get; set; }
            public string last_updated { get; set; }
            public double temp_c { get; set; }
            public double temp_f { get; set; }
            public int is_day { get; set; }
            public Condition condition { get; set; }
            public double wind_mph { get; set; }
            public double wind_kph { get; set; }
            public int wind_degree { get; set; }
            public string wind_dir { get; set; }
            public double pressure_mb { get; set; }
            public double pressure_in { get; set; }
            public double precip_mm { get; set; }
            public double precip_in { get; set; }
            public int humidity { get; set; }
            public int cloud { get; set; }
            public double feelslike_c { get; set; }
            public double feelslike_f { get; set; }
            public double vis_km { get; set; }
            public double vis_miles { get; set; }
            public double uv { get; set; }
            public double gust_mph { get; set; }
            public double gust_kph { get; set; }
        }

        public class Condition2
        {
            public string text { get; set; }
            public string icon { get; set; }
            public int code { get; set; }
        }

        public class Day
        {
            public double maxtemp_c { get; set; }
            public double maxtemp_f { get; set; }
            public double mintemp_c { get; set; }
            public double mintemp_f { get; set; }
            public double avgtemp_c { get; set; }
            public double avgtemp_f { get; set; }
            public double maxwind_mph { get; set; }
            public double maxwind_kph { get; set; }
            public double totalprecip_mm { get; set; }
            public double totalprecip_in { get; set; }
            public double avgvis_km { get; set; }
            public double avgvis_miles { get; set; }
            public double avghumidity { get; set; }
            public Condition2 condition { get; set; }
            public double uv { get; set; }
        }

        public class Astro
        {
            public string sunrise { get; set; }
            public string sunset { get; set; }
            public string moonrise { get; set; }
            public string moonset { get; set; }
        }

        public class Forecastday
        {
            public string date { get; set; }
            public int date_epoch { get; set; }
            public Day day { get; set; }
            public Astro astro { get; set; }
        }

        public class Forecast
        {
            public List<Forecastday> forecastday { get; set; }
        }

        public class RootObject
        {
            public Location location { get; set; }
            public Current current { get; set; }
            public Forecast forecast { get; set; }
        }

        public async static Task<RootObject> GetCurrentWeatherAsync(string city)
        {
            var http = new HttpClient();

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

            var url = String.Format("https://api.apixu.com/v1/forecast.json?key=92dc9ddb929f4ebfa1b134609190604&q={0}", city);
            var respone = await http.GetAsync(url);
            var result = await respone.Content.ReadAsStringAsync();
            var serializer = new DataContractJsonSerializer(typeof(RootObject));

            var ms = new MemoryStream(Encoding.UTF8.GetBytes(result));
            var data = (RootObject)serializer.ReadObject(ms);

            return data;
        }
    }
}
