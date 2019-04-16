using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace WorldWeather
{
    public class ParseWeatherXML
    {
        public static Weather Parse(Stream stream)
        {
            XmlTextReader xmlReader = new XmlTextReader(stream);

            Weather result = new Weather()
            {
                ID = int.MaxValue,
                City = string.Empty,
                MinTemperature = double.NaN,
                MaxTemperature = double.NaN,
                Temperature = double.NaN,
                Humidity = int.MaxValue,
                Pressure = int.MaxValue,
                WindSpeed = double.NaN,
                WindType = string.Empty,
                WindDirection = string.Empty,
                Clouds = string.Empty,
                WeatherId = int.MaxValue,
                IconID = string.Empty,
                LastUpdate = DateTime.MaxValue
            };

            try
            {
                var path = new Uri(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase)).LocalPath;
                XmlSchemaSet schema = new XmlSchemaSet();
                schema.Add("", path + "\\XmlScheme.xsd");
                XmlReader rd = XmlReader.Create(stream);
                XDocument doc = XDocument.Load(rd);

                ValidationEventHandler eventHandler = new ValidationEventHandler(ValidationEventHandler);
                doc.Validate(schema, eventHandler);
            }
            catch (Exception ex)
            {

            }

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
                                result.MaxTemperature = Convert.ToInt32(double.Parse(xmlReader.GetAttribute("max"),
                                    System.Globalization.CultureInfo.InvariantCulture) - 273.15);
                                result.MinTemperature = Convert.ToInt32(double.Parse(xmlReader.GetAttribute("min"),
                                    System.Globalization.CultureInfo.InvariantCulture) - 273.15);
                                result.Temperature = Convert.ToInt32(double.Parse(xmlReader.GetAttribute("value"),
                                    System.Globalization.CultureInfo.InvariantCulture) - 273.15);
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
                                result.WeatherId = int.Parse(xmlReader.GetAttribute("number"),
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

        static void ValidationEventHandler(object sender, ValidationEventArgs e)
        {
            switch (e.Severity)
            {
                case XmlSeverityType.Error:
                    MessageBox.Show("Error: {0}", e.Message);
                    break;
                case XmlSeverityType.Warning:
                    MessageBox.Show("Warning {0}", e.Message);
                    break;
            }
        }
    }
}
