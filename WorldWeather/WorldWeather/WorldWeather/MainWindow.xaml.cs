using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace WorldWeather
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        BackgroundWorker worker = new BackgroundWorker();
        WeatherDbContext db = new WeatherDbContext();
        ObservableCollection<Weather> currentWeather = new ObservableCollection<Weather> { };
        DispatcherTimer dispatcherTimer = new DispatcherTimer();

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Weather> currentWeatherItems
        {
            get => currentWeather;
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            
            foreach (var value in db.Weathers)
            {
                db.Weathers.Remove(value);
            }
            db.SaveChanges();

            this.AddCity("Wroclaw");
            
        }

        
        
        
        public async void AddCity(string city)
        {
            string xmlResponse = await WeatherConnection.LoadWeatherAsync(city);

            Weather result;

            using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(xmlResponse)))
            {
                result = ParseWeatherXML.Parse(stream);

                db.Weathers.Add(result);
                currentWeatherItems.Add(result);
                try
                {
                    db.SaveChanges();
                }
                catch(Exception ex)
                {
                    throw ex;
                }
            }
        }

        private async Task<Weather> GetCityWeather(string city)
        {
            string xmlResponse = await WeatherConnection.LoadWeatherAsync(city);

            Weather result;

            using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(xmlResponse)))
            {
                result = ParseWeatherXML.Parse(stream);
            }

            return result;
        }

        private void LoadWeatherData(object sender, RoutedEventArgs e)
        {
            this.AddCity(nameTextBox.Text);
        }

        private void AddWeatherData(object sender, RoutedEventArgs e)
        {
            currentWeatherItems.Add(new Weather
            {
                ID = int.Parse(IdTextBox.Text),
                City = nameTextBox.Text,
                Temperature = int.Parse(TemperatureTextBox.Text)
            });
            db.Weathers.Add(new Weather
            {
                ID = int.Parse(IdTextBox.Text),
                City = nameTextBox.Text,
                Temperature = int.Parse(TemperatureTextBox.Text)
            });
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void DataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                RebindData();
                SetTimer();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        //Refreshes grid data on timer tick
        protected void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            RebindData();
        }

        //Get data and bind to the grid
        private void RebindData()
        {
            var WroclawCity = from val in db.Weathers
                              where val.City == "Wroclaw"
                              select val;

            if (WroclawCity.Any())
            {
                var CurrentWroclawCity = GetCityWeather("Wroclaw").Result;
                foreach (var item in WroclawCity)
                {
                    Weather weather = Dispatcher.BeginInvoke(DispatcherPriority.Background, new ParameterizedThreadStart(Load));
                }
                //WroclawCity.First().Temperature = CurrentWroclawCity.Result.Temperature;
                //WroclawCity.First().MinTemperature = CurrentWroclawCity.Result.MinTemperature;
                //WroclawCity.First().MaxTemperature = CurrentWroclawCity.Result.MaxTemperature;
                //WroclawCity.First().Pressure = CurrentWroclawCity.Result.Pressure;
                //WroclawCity.First().Humidity= CurrentWroclawCity.Result.Humidity;
                //WroclawCity.First().Clouds = CurrentWroclawCity.Result.Clouds;
                //WroclawCity.First().LastUpdate = CurrentWroclawCity.Result.LastUpdate;
                //WroclawCity.First().WindSpeed = CurrentWroclawCity.Result.WindSpeed;
                //WroclawCity.First().WindType = CurrentWroclawCity.Result.WindType;
                //WroclawCity.First().WindDirection = CurrentWroclawCity.Result.WindDirection;

                grid.Items.Refresh();
                //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Weather.Temperature)));
                db.SaveChanges();
            }
        }

        //Set and start the timer
        private void SetTimer()
        {
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 5);
            dispatcherTimer.Start();
        }

        
    }
}
