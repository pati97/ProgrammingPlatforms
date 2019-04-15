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
        int? weatherId;
        string iconId;
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Weather> currentWeatherItems
        {
            get => currentWeather;
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            //grid.ItemsSource = db.Weathers.ToList();
            
            this.AddCity("Wroclaw");
            grid.ItemsSource = db.Weathers.ToList();
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


        public async void AddCity(string city)
        {
            string xmlResponse = await WeatherConnection.LoadWeatherAsync(city);

            Weather result;

            using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(xmlResponse)))
            {
                result = ParseWeatherXML.Parse(stream);
                weatherId = result.WeatherId;
                iconId = result.IconID;
                db.Weathers.Add(result);
                currentWeatherItems.Add(result); 
                try
                {
                    db.SaveChanges();
                    grid.ItemsSource = db.Weathers.ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            if (!weatherId.HasValue)
            {

                Uri url = new Uri("pack://application:,,,/WorldWeather;component/WeatherIcons/atmosphere.png");
                WeatherImage.Source = new BitmapImage(url);
            }
            else
            {
                var id = (int)(weatherId);
                char timePeriod = char.MinValue;
      
                var pack = "pack://application:,,,/WorldWeather;component/WeatherIcons/";
                if (iconId.Contains('d'))
                {
                    timePeriod = 'd';
                }
                else if(iconId.Contains('n'))
                    timePeriod = 'n';
                
                var img = string.Empty;

                if (id >= 200 && id < 300) img = "thunderstorm.png";
                else if (id >= 300 && id < 500) img = "drizzle.png";
                else if (id >= 500 && id < 600) img = "rain.png";
                else if (id >= 600 && id < 700) img = "snow.png";
                else if (id >= 700 && id < 800) img = "atmosphere.png";
                else if (id == 800) img = (timePeriod == 'd') ? "clear_day.png" : "clear_night.png";
                else if (id == 801) img = (timePeriod == 'd') ? "few_clouds_day.png" : "few_clouds_night.png";
                else if (id == 802 || id == 803) img = (timePeriod == 'd') ? "broken_clouds_day.png" : "broken_clouds_night.png";
                else if (id == 804) img = "overcast_clouds.png";
                else if (id >= 900 && id < 903) img = "extreme.png";
                else if (id == 903) img = "cold.png";
                else if (id == 904) img = "hot.png";
                else if (id == 905 || id >= 951) img = "windy.png";
                else if (id == 906) img = "hail.png";

                Uri source = new Uri(pack + img);

                BitmapImage bmp = new BitmapImage();
                bmp.BeginInit();
                bmp.UriSource = source;
                bmp.CacheOption = BitmapCacheOption.OnLoad;
                bmp.EndInit();


                WeatherImage.Source = bmp;
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
            grid.ItemsSource = db.Weathers.ToList();
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
                grid.ItemsSource = db.Weathers.ToList();
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
            var WroclawCity = from row in db.Weathers
                              select row;
            
            if (WroclawCity.Any())
            {
                foreach (var item in WroclawCity)
                {
                    var task = Task.Run(async () => await GetCityWeather(item.City));

                    //MessageBox.Show("Wroclaw City temp = " + task.Result.Temperature.ToString());
                    item.Temperature += task.Result.Temperature;
                    task.Wait();
                }
                grid.Items.Refresh();
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
