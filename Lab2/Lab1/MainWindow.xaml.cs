using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
        async Task<int> GetNumberAsync(int number)
        {
            if (number < 0)
                throw new ArgumentOutOfRangeException("number", number, "The number must be greater or equal zero");
            int result = 0;
            while (result < number)
            {
                result++;
                await Task.Delay(100);
            }
            return number;
        }

        protected void UpdateProgressBlock(string text, TextBlock textBlock)
        {
            try
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    textBlock.Text = text;
                });
            }
            catch { }
        }

        class WaitingAnimation
        {
            private int maxNumberOfDots;
            private int currentDots;
            private MainWindow sender;


            public WaitingAnimation(int maxNumberOfDots, MainWindow sender)
            {
                this.maxNumberOfDots = maxNumberOfDots;
                this.sender = sender;
                currentDots = 0;
            }

            public void CheckStatus(Object stateInfo)
            {
                sender.UpdateProgressBlock(
                    "Processing" +
                    new Func<string>(() => {
                        StringBuilder strBuilder = new StringBuilder(string.Empty);
                        for (int i = 0; i < currentDots; i++)
                            strBuilder.Append(".");
                        return strBuilder.ToString();
                    })(), sender.progressTextBlock
                );
                if (currentDots == maxNumberOfDots)
                    currentDots = 0;
                else
                    currentDots++;
            }
        }


        ObservableCollection<Weather> weather = new ObservableCollection<Weather>{};

        public ObservableCollection<Weather> WeatherItems
        {
            get => weather;
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Clear()
        {
            CityTxB.Clear();  
        }
 
        private async void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var rootObject = await Weather.GetWeather(CityTxB.Text);
            try
            {
                int finalNumber = 25;
                var getResultTask = GetNumberAsync(finalNumber);
                var waitingAnimationTask =
                    new System.Threading.Timer(
                        new WaitingAnimation(5, this).CheckStatus,
                        null,
                        TimeSpan.FromMilliseconds(0),
                        TimeSpan.FromMilliseconds(500)
                    );
                int result = await getResultTask;
                waitingAnimationTask.Dispose();
            }
            catch (Exception ex)
            {
                this.progressTextBlock.Text = "Error! " + ex.Message;
            }

            try
            {
                weather.Add(new Weather
                {
                    City = rootObject.location.name,
                    Temperature = rootObject.current.temp_c,
                    Description = rootObject.current.condition.text,
                    Humidity = rootObject.current.humidity,
                    Cloud = rootObject.current.cloud,
                    Wind = rootObject.current.wind_kph
                });
            }
            catch(Exception ex)
            {
                this.progressTextBlock.Text = "Error! " + ex.Message;
            }

            this.progressTextBlock2.Text = "Added " + CityTxB.Text + ".";
            this.progressTextBlock.Text = String.Empty;
        }
    }
}
