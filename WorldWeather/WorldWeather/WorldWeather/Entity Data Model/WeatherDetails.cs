//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WorldWeather.Entity_Data_Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class WeatherDetails
    {
        public int IdWeather { get; set; }
        public string City { get; set; }
        public string Description { get; set; }
        public Nullable<int> ID { get; set; }
        public string IconID { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string WindDirection { get; set; }
        public Nullable<double> WindSpeed { get; set; }
        public Nullable<double> Temperature { get; set; }
        public Nullable<double> MaxTemperature { get; set; }
        public Nullable<double> MinTemperature { get; set; }
        public Nullable<double> Pressure { get; set; }
        public Nullable<double> Humidity { get; set; }
    }
}
