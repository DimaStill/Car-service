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
using System.Windows.Documents;
using Word = Microsoft.Office.Interop.Word;

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
        DataSet ds = new DataSet();

        public MainWindow()
        {
            InitializeComponent();

            ordersGrid.DataContext = ds.Tables[0];
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
                try
                {
                    string sqlCommand = "SELECT * FROM [Замовлення]";
                    SqlCommand command = new SqlCommand(sqlCommand, connection);
                    adapter = new SqlDataAdapter(command);
                    connection.Open();
                    ds.Clear();
                    adapter.Fill(ds);
                    ordersGrid.ItemsSource = ds.Tables[0].DefaultView;
                    /*adapter.Fill(tableOrders);
                    ordersGrid.ItemsSource = tableOrders.DefaultView;*/

                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, "Помилка!");
                }
            }
        }

        private void OnDeleteClick(object sender, RoutedEventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    DataRowView row = (DataRowView)ordersGrid.SelectedItems[0];
                    string sqlCommand = "DELETE FROM [Замовлення] WHERE [Id] = " + row["Id"];
                    SqlCommand command = new SqlCommand(sqlCommand, connection);
                    adapter = new SqlDataAdapter(command);
                    connection.Open();
                    int amountAdd = command.ExecuteNonQuery();
                    if (amountAdd > 0)
                        new MessageWindow("Видалення успішно виконано", "Warning").Show();
                    else
                        new MessageWindow("Не вдалося видалити", "Error").Show();
                    connection.Close();
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, "Помилка!");
                }
            }
        }

        private void OnPrintClick(object sender, RoutedEventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    DataRowView row = (DataRowView)ordersGrid.SelectedItems[0];
                    string sqlCommand = "SELECT * FROM [Замовлення] WHERE [Id] = " + row["Id"];
                    SqlCommand command = new SqlCommand(sqlCommand, connection);
                    SqlDataReader sqlDataReader = command.ExecuteReader();

                    Word.Document doc = null;
                    Word.Application app = new Word.Application();
                    string source = Environment.CurrentDirectory + "\\Example.docx";
                    doc = app.Documents.Open(source);
                    doc.Activate();

                    Word.Bookmarks wBookmarks = doc.Bookmarks;
                    Word.Range wRange;
                    sqlDataReader.Read();
                    wRange = wBookmarks[1].Range;
                    wRange.Text = sqlDataReader.GetValue(1).ToString() + sqlDataReader.GetValue(2).ToString();
                    wRange = wBookmarks[2].Range;
                    wRange.Text = sqlDataReader.GetValue(5).ToString().Split(' ')[0];
                    wRange = wBookmarks[3].Range;
                    wRange.Text = sqlDataReader.GetValue(8).ToString();
                    wRange = wBookmarks[4].Range;
                    wRange.Text = sqlDataReader.GetValue(3).ToString();
                    wRange = wBookmarks[5].Range;
                    wRange.Text = sqlDataReader.GetValue(0).ToString();
                    wRange = wBookmarks[6].Range;
                    wRange.Text = sqlDataReader.GetValue(10).ToString();
                    wRange = wBookmarks[7].Range;
                    wRange.Text = sqlDataReader.GetValue(9).ToString();

                    PrintDialog printDialog = new PrintDialog();
                    if (printDialog.ShowDialog() == true)
                    {
                        printDialog.PrintDocument(((IDocumentPaginatorSource)doc).DocumentPaginator, "Document");
                    }
                    doc.Close();

                    doc = null;
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, "Помилка!");
                }
            }
            
           
        }

        private void TextBoxGotFocus(object sender, RoutedEventArgs e)
        {
            searchTextBox.Text = String.Empty;
        }

        private void SearchPreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (searchTextBox.Text != String.Empty)
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        string sqlCommand = "SELECT * FROM [Замовлення] WHERE [" + SearchField.Text + "] LIKE N'" + searchTextBox.Text + "%'";
                        SqlCommand command = new SqlCommand(sqlCommand, connection);
                        adapter = new SqlDataAdapter(command);
                        connection.Open();
                        ds.Clear();
                        adapter.Fill(ds);
                        ordersGrid.ItemsSource = ds.Tables[0].DefaultView;
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.Message, "Помилка!");
                    }
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
                    adapter.Fill(ds);
                    ordersGrid.ItemsSource = ds.Tables[0].DefaultView;
                    /*adapter.Fill(tableOrders);
                    ordersGrid.ItemsSource = tableOrders.DefaultView;*/

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
