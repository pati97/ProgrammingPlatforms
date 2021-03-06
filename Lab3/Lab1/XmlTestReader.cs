﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Lab1
{
    public class XmlTestReader
    {
        public static WeatherData Parse(Stream stream)
        {
            XmlTextReader reader = new XmlTextReader(stream);
            WeatherData result = new WeatherData()
            {
                City = string.Empty,
                Temperature = double.NaN,
                Pressure = int.MaxValue,
                Humidity = int.MaxValue,
                Description = string.Empty,
                Wind = double.NaN,
            };

            while(reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        switch (reader.Name)
                        {
                            case "name":
                                result.City = reader.ReadElementContentAsString();
                                break;
                            case "temp_c":
                                result.Temperature =
                                    double.Parse(
                                        reader.ReadElementContentAsString(),
                                       System.Globalization.CultureInfo.InvariantCulture);
                                break;
                            case "pressure_mb":
                                result.Pressure =
                                    double.Parse(
                                        reader.ReadElementContentAsString(),
                                        System.Globalization.CultureInfo.InvariantCulture);
                               break;
                            case "text":
                                result.Description = reader.ReadElementContentAsString();
                                break;
                            
                            case "humidity":
                                result.Humidity =
                                    double.Parse(
                                        reader.ReadElementContentAsString(),
                                        System.Globalization.CultureInfo.InvariantCulture);
                                break;
                            case "wind_kph":
                                result.Wind =
                                    double.Parse(
                                        reader.ReadElementContentAsString(),
                                        System.Globalization.CultureInfo.InvariantCulture);
                                break;
                        }
                        break;
                }
            }
            return result;
        }
    }
}
