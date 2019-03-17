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
        private string filename;

        ObservableCollection<Person> people = new ObservableCollection<Person>
        {
            new Person {Name = "Bartosz", Age = 3, Image = @"C:\Users\pati\Documents\zdjecia\bartus.jpg"}
        };

        public ObservableCollection<Person> PersonItems
        {
            get => people;
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Clear()
        {
            NameTxB.Clear();
            AgeTxB.Clear();
            filename = null;
            Image.Source = null;
        }
        private void listViewItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Person selectedRow = (Person)MyListView.SelectedItem;

            if (selectedRow != null)
            {
                NameTxB.Text = selectedRow.Name;
                AgeTxB.Text = selectedRow.Age.ToString();
                filename = selectedRow.Image;
                Uri uri = new Uri(filename);
                BitmapImage bmp = new BitmapImage(uri);
                Image.Source = bmp;
            }
            
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(NameTxB.Text) &&
                !String.IsNullOrEmpty(AgeTxB.Text) &&
                !String.IsNullOrEmpty(filename))
            {
                people.Add(new Person { Age = int.Parse(AgeTxB.Text), Name = NameTxB.Text, Image = filename });
                Clear(); 
            }
        }

        private void SelectButton_Click(object sender, RoutedEventArgs e)
        {
            Clear();
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.FileName = "Document"; // Default file name
            fileDialog.DefaultExt = ".jpg"; // Default file extension
            fileDialog.Filter = "Images (.jpg)|*.jpg"; // Filter files by extension

            Nullable<bool> result = fileDialog.ShowDialog();

            if (result == true)
            {
                // Open document
                filename = fileDialog.FileName;
                Image.Source = new BitmapImage(new Uri(filename));
            }
            else
            {
                filename = null;
            }
        }
    }
}
