using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace WorldWeather
{
    public class ParseWeatherXML
    {
        public static Weather Parse(Stream stream)
        {
            XmlTextReader xmlReader = new XmlTextReader(stream);

            Weather result = new Weather()
            {
                City = string.Empty,
                Temperature = double.NaN,
                ID = int.MaxValue,
                IconID = string.Empty
            };

            while (xmlReader.Read())
            {
                switch (xmlReader.NodeType)
                {
                    case XmlNodeType.Element:
                        switch (xmlReader.Name)
                        {
                            case "city":
                                result.City = xmlReader.GetAttribute("name");
                                break;
                            case "temperature":
                                result.MaxTemperature = double.Parse(xmlReader.GetAttribute("max"),
                                    System.Globalization.CultureInfo.InvariantCulture);
                                result.MinTemperature = double.Parse(xmlReader.GetAttribute("min"),
                                    System.Globalization.CultureInfo.InvariantCulture);
                                result.Temperature = double.Parse(xmlReader.GetAttribute("value"),
                                    System.Globalization.CultureInfo.InvariantCulture);
                                break;
                            case "humidity":
                                result.Humidity = int.Parse(xmlReader.GetAttribute("value"),
                                    System.Globalization.CultureInfo.InvariantCulture);
                                break;
                            case "pressure":
                                result.Pressure = int.Parse(xmlReader.GetAttribute("value"),
                                    System.Globalization.CultureInfo.InvariantCulture);
                                break;
                            case "speed":
                                result.WindType = xmlReader.GetAttribute("name");
                                result.WindSpeed = double.Parse(xmlReader.GetAttribute("value"),
                                    System.Globalization.CultureInfo.InvariantCulture);
                                break;
                            case "direction":
                                result.WindDirection = xmlReader.GetAttribute("name");
                                break;
                            case "clouds":
                                result.Clouds = xmlReader.GetAttribute("name");
                                break;
                            case "weather":
                                result.ID = int.Parse(xmlReader.GetAttribute("number"),
                                    System.Globalization.CultureInfo.InvariantCulture);
                                result.IconID = xmlReader.GetAttribute("icon");
                                break;
                            case "lastupdate":
                                result.LastUpdate = DateTime.Parse(xmlReader.GetAttribute("value"),
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
