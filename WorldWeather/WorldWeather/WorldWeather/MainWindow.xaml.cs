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
        ObservableCollection<CurrentWeather> currentWeather = new ObservableCollection<CurrentWeather> { };

        public ObservableCollection<CurrentWeather> currentWeatherItems
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
            var rootObject = await CurrentWeather.GetCurrentWeatherAsync(nameTextBox.Text);

            currentWeather.Add(new CurrentWeather
            {
                City = rootObject.location.name,
                Temperature = rootObject.current.temp_c
            });
        }
    }
}
