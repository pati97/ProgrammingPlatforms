using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
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
            worker.DoWork += DoWork;
            worker.ProgressChanged += ProgressChanged;

        }

        private void Clear()
        {
            //CityTxB.Clear();
            //ProgressTextBlock.Text = string.Empty;
            //worker.ReportProgress(0);
        }

        private async void LoadWeatherData(object sender, RoutedEventArgs e)
        {
            //Clear();
            try
            {
                if (CityTxB.Text != string.Empty)
                {
                    string responseXML = await WeatherConnection.LoadDataAsync(CityTxB.Text);
                    WeatherData result;
                    worker.ReportProgress(0,("Loading " + CityTxB.Text + "..."));
                
                    using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(responseXML)))
                    {
                        result = XmlTestReader.Parse(stream);
                        WeatherItems.Add(new WeatherData()
                        {
                            City = result.City,
                            Temperature = result.Temperature,
                            Humidity = result.Humidity,
                            Description = result.Description,
                            Pressure = result.Pressure,
                            Wind = result.Wind
                        });
                    }
                    
                    worker.ReportProgress(100,"Done");
                    //worker.ReportProgress(50);    
                    CityTxB.Text = string.Empty;  
                }     
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            

        }
        private void LoadListCityData(object sender, RoutedEventArgs e)
        {
            if (worker.IsBusy != true)
                worker.RunWorkerAsync();
        }
        private void ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.ProgressBar.Value = e.ProgressPercentage;
            this.ProgressTextBlock.Text = e.UserState as string;
        }

        private void DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            Clear();
            List<string> cities = new List<string> {
                "London", "Berlin", "Paris", "Wroclaw", "Pekin" };
            for (int i = 0; i < cities.Count; i++)
            {
                string city = cities[i];

                if (worker.CancellationPending == true)
                {
                    worker.ReportProgress(0, "Cancelled");
                    e.Cancel = true;
                    return;
                }
                else
                {
                    worker.ReportProgress(
                        (int)Math.Round((float)(i + 1) * 100.0 / (float)cities.Count),
                        "Loading " + city + "...");
                    string responseXML = WeatherConnection.LoadDataAsync(city).Result;
                    WeatherData result;

                    using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(responseXML)))
                    {
                        result = XmlTestReader.Parse(stream);
                        AddWeather(
                            new WeatherData()
                            {
                                City = result.City,
                                Temperature = result.Temperature,
                                Humidity = result.Humidity,
                                Description = result.Description,
                                Pressure = result.Pressure,
                                Wind = result.Wind
                            });
                    }
                    Thread.Sleep(2000);
                }
            }
            worker.ReportProgress(100, "Done");
            Thread.Sleep(2000);
            worker.ReportProgress(0);
        }
    }
}