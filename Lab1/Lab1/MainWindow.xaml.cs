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
        private DataTable dt;
        private string filename;
        DataGrid MyDataGrid;

        public MainWindow()
        {
            InitializeComponent();

            MyDataGrid = new DataGrid();
            MyDataGrid.AutoGenerateColumns = false;
            
            DataGridTextColumn col1 = new DataGridTextColumn();
            col1.Header = "Name";
            col1.Binding = new Binding("Name");
            col1.Width = 70;
            MyDataGrid.Columns.Add(col1);

            DataGridTextColumn col2 = new DataGridTextColumn();
            col2.Header = "Age";
            col2.Binding = new Binding("Age");
            col2.Width = 70;
            MyDataGrid.Columns.Add(col2);

            DataGridTemplateColumn col3 = (DataGridTemplateColumn)FindResource("image");
            MyDataGrid.Columns.Add(col3);

            dt = new DataTable();
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Age", typeof(int));
            dt.Columns.Add("Image", typeof(BitmapImage));

            MyDataGrid.AddHandler(MouseDoubleClickEvent, new RoutedEventHandler(CopyCell_Click));

            MyDataGrid.ItemsSource = dt.DefaultView;
            BackgroundGrid.Children.Add(MyDataGrid);
        }

        private void clear()
        {
            NameTxB.Clear();
            AgeTxB.Clear();
            filename = null;
            Image.Source = null;
        }

        private void CopyCell_Click(object sender, RoutedEventArgs e)
        {
            DataRowView itemSource = MyDataGrid.CurrentItem as DataRowView;
            NameTxB.Text = itemSource.Row.ItemArray[0].ToString();
            AgeTxB.Text = itemSource.Row.ItemArray[1].ToString();     
        }

        private void Row_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Image img = sender as Image;

            if (img != null)
            {
                Image.Source = img.Source;
            }
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(NameTxB.Text) &&
                !String.IsNullOrEmpty(AgeTxB.Text) &&
                Image.Source != null)
            {
                Uri uri = new Uri(filename);
                BitmapImage bmp = new BitmapImage(uri);

                DataRow dr = dt.NewRow();
                dr[0] = NameTxB.Text;
                dr[1] = int.Parse(AgeTxB.Text);
                dr[2] = bmp;
                dt.Rows.Add(dr);

                clear();
            }
        }

        private void SelectButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.FileName = "Document"; // Default file name
            fileDialog.DefaultExt = ".jpg"; // Default file extension
            fileDialog.Filter = "Text documents (.jpg)|*.jpg"; // Filter files by extension

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
