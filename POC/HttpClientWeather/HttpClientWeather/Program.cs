using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;

namespace HttpClientWeather
{
    //[DataContract]
    //public class Coord
    //{
    //    [DataMember]
    //    public double lon { get; set; }

    //    [DataMember]
    //    public double lat { get; set; }
    //}

    //[DataContract]
    //public class Weather
    //{
    //    public int id { get; set; }
    //    public string main { get; set; }
    //    public string description { get; set; }
    //    public string icon { get; set; }
    //}

    //[DataContract]
    //public class Main
    //{
    //    public double temp { get; set; }
    //    public int pressure { get; set; }
    //    public int humidity { get; set; }
    //    public double temp_min { get; set; }
    //    public double temp_max { get; set; }
    //}

    //[DataContract]
    //public class Wind
    //{
    //    public double speed { get; set; }
    //    public int deg { get; set; }
    //}

    //[DataContract]
    //public class Clouds
    //{
    //    public int all { get; set; }
    //}

    //[DataContract]
    //public class Sys
    //{
    //    public int type { get; set; }
    //    public int id { get; set; }
    //    public double message { get; set; }
    //    public string country { get; set; }
    //    public int sunrise { get; set; }
    //    public int sunset { get; set; }
    //}

    //[DataContract]
    //public class RootObject
    //{
    //    public Coord coord { get; set; }
    //    public List<Weather> weather { get; set; }
    //    public string @base { get; set; }
    //    public Main main { get; set; }
    //    public int visibility { get; set; }
    //    public Wind wind { get; set; }
    //    public Clouds clouds { get; set; }
    //    public int dt { get; set; }
    //    public Sys sys { get; set; }
    //    public int id { get; set; }
    //    public string name { get; set; }
    //    public int cod { get; set; }
    //}

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

    public class RootObject
    {
        public Location location { get; set; }
        public Current current { get; set; }
    }
    public class WeatherProxy
    {
        public async static Task<RootObject> GetLondonWeather()
        {
            var http = new HttpClient();
            var url = String.Format("http://api.apixu.com/v1/current.json?key=78ae3fdd414d4f168ed130602191703&q=London");
            var respone = await http.GetAsync(url);
            var result = await respone.Content.ReadAsStringAsync();
            var serializer = new DataContractJsonSerializer(typeof(RootObject));

            var ms = new MemoryStream(Encoding.UTF8.GetBytes(result));
            var data = (RootObject)serializer.ReadObject(ms);

            return data;
        }
    }

    class Program
    {
        public async static void Index()
        {
            var weather = await WeatherProxy.GetLondonWeather();

            Console.WriteLine("temp = {0}, wind = {1},desc = {2}, name = {3}",
                              ((int)weather.current.temp_c).ToString() + "C",
                              ((int)weather.current.wind_kph).ToString() ,
                              weather.current.condition.text,
                              weather.location.name);
        }

        static void Main(string[] args)
        {
            Index();
            Console.ReadKey();
        }
    }
}
