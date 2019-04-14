using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace WorldWeather
{
    public class Weather
    { 
        public int ID { get; set; }
        public string City { get; set; }
        public double MinTemperature { get; set; }
        public double MaxTemperature { get; set; }
        public double Temperature { get; set; }
        public int Humidity { get; set; }
        public int Pressure { get; set; }
        public double WindSpeed { get; set; }
        public string WindType { get; set; }
        public string WindDirection { get; set; }
        public string Clouds { get; set; }
        public int WeatherId { get; set; }
        public string IconID { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}
