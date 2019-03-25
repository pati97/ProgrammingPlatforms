using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
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

namespace Lab1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BackgroundWorker worker = new BackgroundWorker();

        ObservableCollection<WeatherData> weather = new ObservableCollection<WeatherData>
        {
        };

        public ObservableCollection<WeatherData> WeatherItems
        {
            get => weather;
        }

        public void AddWeather(WeatherData weather)
        {
            Application.Current.Dispatcher.Invoke(() => { WeatherItems.Add(weather); });
        }
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
            //worker.DoWork += DoWork;
           // worker.ProgressChanged += ProgressChanged;
            
        }

        private void Clear()
        {
            CityTxB.Clear();  
        }
 
        private async void LoadWeatherData(object sender, RoutedEventArgs e)
        {
            string responseXML = await WeatherConnection.LoadDataAsync(CityTxB.Text);
            WeatherData result;

            using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(responseXML)))
            {
                result = XmlTestReader.Parse(stream);
                WeatherItems.Add(new WeatherData()
                {
                    City = "StreamParser: " + result.City,
                    Temperature = result.Temperature,
                    Humidity = result.Humidity,
                    Description = result.Description,
                    Cloud = result.Cloud,
                    Wind = result.Wind      
                });
            }
            if (worker.IsBusy != true)
                worker.RunWorkerAsync();
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //weatherDataProgressBar.Value = e.ProgressPercentage;
           // weatherDataTextBlock.Text = e.UserState as string;
        }

        //private async void AddButton_Click(object sender, RoutedEventArgs e)
        //{
        //    var rootObject = await Weather.GetWeather(CityTxB.Text);
        //    try
        //    {
        //        int finalNumber = 25;
        //        var getResultTask = GetNumberAsync(finalNumber);
        //        var waitingAnimationTask =
        //            new System.Threading.Timer(
        //                new WaitingAnimation(5, this).CheckStatus,
        //                null,
        //                TimeSpan.FromMilliseconds(0),
        //                TimeSpan.FromMilliseconds(500)
        //            );
        //        int result = await getResultTask;
        //        waitingAnimationTask.Dispose();
        //    }
        //    catch (Exception ex)
        //    {
        //        this.progressTextBlock.Text = "Error! " + ex.Message;
        //    }

        //    try
        //    {
        //        weather.Add(new Weather
        //        {
        //            City = rootObject.location.name,
        //            Temperature = rootObject.current.temp_c,
        //            Description = rootObject.current.condition.text,
        //            Humidity = rootObject.current.humidity,
        //            Cloud = rootObject.current.cloud,
        //            Wind = rootObject.current.wind_kph
        //        });
        //    }
        //    catch(Exception ex)
        //    {
        //        this.progressTextBlock.Text = "Error! " + ex.Message;
        //    }

        //    this.progressTextBlock2.Text = "Added " + CityTxB.Text + ".";
        //    this.progressTextBlock.Text = String.Empty;
        //}
    }
}
