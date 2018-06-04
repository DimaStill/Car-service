using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
using System.Windows.Shapes;

namespace Car_service
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string connectionString;
        SqlDataAdapter adapter;
        DataTable tableOrders, tableCars, tableClients;

        public MainWindow()
        {
            InitializeComponent();
        }

        public MainWindow(User currentUser)
        {
            InitializeComponent();
            DataContext = currentUser;
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        private void WindowSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.NewSize.Height < 525 || e.NewSize.Width < 1025)
            {
                Width = e.PreviousSize.Width;
                Height = e.PreviousSize.Height;
            }
            else
            {
                double tabSize = tabSize = Width / 4 - 4;
                if (Double.IsNaN(Width))
                    tabSize = 252.25;
                TabPage1.Width = tabSize;
                TabPage2.Width = tabSize;
                TabPage3.Width = tabSize;
                TabPage4.Width = tabSize;
            }
        }

        private void OnRefreshClick(object sender, RoutedEventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlCommand = "SELECT * FROM [Замовлення]";
                SqlCommand command = new SqlCommand(sqlCommand, connection);
                adapter = new SqlDataAdapter(command);
                connection.Open();

                ordersGrid.ItemsSource = null;
                adapter.Fill(tableOrders);
                ordersGrid.ItemsSource = tableOrders.DefaultView;
            }
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            
            tableOrders = new DataTable();
            tableCars = new DataTable();
            tableClients = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    string sqlCommand = "SELECT * FROM [Замовлення]";
                    SqlCommand command = new SqlCommand(sqlCommand, connection);
                    adapter = new SqlDataAdapter(command);
                    connection.Open();
                    adapter.Fill(tableOrders);
                    ordersGrid.ItemsSource = tableOrders.DefaultView;

                    sqlCommand = "SELECT * FROM [Клієнти]";
                    command = new SqlCommand(sqlCommand, connection);
                    adapter = new SqlDataAdapter(command);
                    adapter.Fill(tableClients);
                    clientsGrid.ItemsSource = tableClients.DefaultView;

                    sqlCommand = "SELECT * FROM [Автомобілі]";
                    command = new SqlCommand(sqlCommand, connection);
                    adapter = new SqlDataAdapter(command);
                    adapter.Fill(tableCars);
                    carGrid.ItemsSource = tableCars.DefaultView;

                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, "Помилка!");
                }
            }
        }

        private void OnAddClick(object sender, RoutedEventArgs e)
        {
            new AddOrderWindow().Show();
        }
    }
}
