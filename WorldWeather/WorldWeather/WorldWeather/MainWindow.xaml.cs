using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        ObservableCollection<Weather> currentWeather = new ObservableCollection<Weather> { };
        
        public ObservableCollection<Weather> currentWeatherItems
        {
            get => currentWeather;
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            //db.Weather.Local.Add(new Weather()
            //{
            //    City = "xd",
            //    ID = 1
            //});
            
            //db.SaveChanges();
        }

        private async void LoadWeatherData(object sender, RoutedEventArgs e)
        {
            var rootObject = await Weather.GetCurrentWeatherAsync(nameTextBox.Text);

            currentWeather.Add(new Weather
            {
                City = rootObject.location.name,
                Temperature = rootObject.current.temp_c
            });
        }
    }
}
