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
        public string City { get; set; }
        public string Description { get; set; }
        public int ID { get; set; }
        public string IconID { get; set; }
        public DateTime Date { get; set; }
        public string WindType { get; set; }
        public string WindDirection { get; set; }
        public double WindSpeed { get; set; }
        public double Temperature { get; set; }
        public double MaxTemperature { get; set; }
        public double MinTemperature { get; set; }
        public double Pressure { get; set; }
        public double Humidity { get; set; }

    }
}
