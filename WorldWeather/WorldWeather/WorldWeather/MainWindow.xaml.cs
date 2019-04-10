using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WorldWeather
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        WeatherDbContext db = new WeatherDbContext();
        ObservableCollection<Weather> currentWeather = new ObservableCollection<Weather> { };
        
        public ObservableCollection<Weather> currentWeatherItems
        {
            get => currentWeather;
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private async void LoadWeatherData(object sender, RoutedEventArgs e)
        {
            string xmlResponse = await WeatherConnection.LoadWeatherAsync(nameTextBox.Text);
            Weather result;

            using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(xmlResponse)))
            {
                result = ParseWeatherXML.Parse(stream);

                currentWeatherItems.Add(new Weather()
                {
                    City = result.City,
                    Temperature = result.Temperature,
                });
            }

            db.Weathers.Add(new Weather()
            {
                ID = 1,
                City = result.City,
                IconID = "cos",
                Temperature = result.Temperature
            });

            db.SaveChanges();
        }
    }
}
