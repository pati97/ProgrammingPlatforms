using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WorldWeather;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace WorldWeatherTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task TestMethod1()
        {
            string xmlResponse = await WeatherConnection.LoadWeatherAsync("Wroclaw");

            Weather result;

            using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(xmlResponse)))
            {
                result = ParseWeatherXML.Parse(stream);
            }

            Assert.AreEqual("Wroclaw", result.City);
        }

        [TestMethod]
        public void TestMethod2()
        {
            Weather result;
            using (FileStream fs = File.OpenRead(@"C:\Users\pati\Desktop\semestr 6\Platformy .Net i Java\ProgrammingPlatforms\WorldWeather\WorldWeather\WorldWeatherTests\DataWroclaw.xml"))
            {
                result = ParseWeatherXML.Parse(fs);
            }
            Assert.AreEqual("Wroclaw", result.City);
            Assert.AreEqual(6, result.Temperature);
            Assert.AreEqual(3, result.MinTemperature);
            Assert.AreEqual(9, result.MaxTemperature);
            Assert.AreEqual(36, result.Humidity);
            Assert.AreEqual(1024, result.Pressure);
            Assert.AreEqual("East-northeast", result.WindDirection);
            Assert.AreEqual(3.1, result.WindSpeed);
            Assert.AreEqual("Light breeze", result.WindType);
            Assert.AreEqual("clear sky", result.Clouds);
        }
    }
}
