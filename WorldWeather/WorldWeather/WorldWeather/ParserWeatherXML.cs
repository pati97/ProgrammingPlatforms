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
                                result.Temperature = double.Parse(xmlReader.GetAttribute("value"),
                                    System.Globalization.CultureInfo.InvariantCulture);
                                break;
                            case "weather":
                                result.ID = int.Parse(xmlReader.GetAttribute("number"),
                                    System.Globalization.CultureInfo.InvariantCulture);
                                result.IconID = xmlReader.GetAttribute("icon");
                                break;
                        }
                        break;
                }
            }

            return result;
        }
    }
}
