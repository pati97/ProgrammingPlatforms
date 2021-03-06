﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;

namespace Lab1
{
    public class Weather
    {
        public RootObject root;
        public string City { get; set; }
        public double Temperature { get; set; }
        public string Description { get; set; }
        public int Cloud { get; set; }
        public double Wind { get; set; }
        public int Humidity { get; set; }

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

            public async static Task<RootObject> GetWeather(string city)
            {
                var http = new HttpClient();
                var url = String.Format("http://api.apixu.com/v1/current.json?key=78ae3fdd414d4f168ed130602191703&q={0}", city);
                var respone = await http.GetAsync(url);
                var result = await respone.Content.ReadAsStringAsync();
                var serializer = new DataContractJsonSerializer(typeof(RootObject));

                var ms = new MemoryStream(Encoding.UTF8.GetBytes(result));
                var data = (RootObject)serializer.ReadObject(ms);

                return data;
            }
    }
}
