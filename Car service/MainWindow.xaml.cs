using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
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
        public static CarServiceContext db;

        List<string> attributesClientsField = new List<string>
        {
            "Id", "Ім’я", "Прізвище",
            "По-батькові", "Телефон", "Email"
        };

        List<string> attributesCarsField = new List<string>
        {
           "Id", "Марка", "Модель", "Тип",
            "Рік випуску", "Державний номер", "VIN"
        };

        public MainWindow()
        {
            InitializeComponent();
        }

        public MainWindow(User currentUser)
        {
            InitializeComponent();
            DataContext = currentUser;

            db = new CarServiceContext();
            db.Clients.Load();
            db.Cars.Load();
            
            clientsGrid.ItemsSource = db.Clients.Local.ToBindingList();
            carGrid.ItemsSource = db.Cars.Local.ToBindingList();

            foreach (var attribute in attributesClientsField)
                SearchFieldClients.Items.Add(attribute);
            foreach (var attribute in attributesCarsField)
                SearchFieldCars.Items.Add(attribute);
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
                if (double.IsNaN(Width))
                    tabSize = 252.25;
                TabPage1.Width = tabSize;
                TabPage2.Width = tabSize;
                TabPage3.Width = tabSize;
                TabPage4.Width = tabSize;
            }
        }

        private void OnRefreshClick(object sender, RoutedEventArgs e)
        {
            clientsGrid.ItemsSource = db.Clients.Local.ToBindingList();
            carGrid.ItemsSource = db.Cars.Local.ToBindingList();
        }

        private void OnDeleteClick(object sender, RoutedEventArgs e)
        {
            int selectedIndex = tabControl1.SelectedIndex;
            switch (selectedIndex)
            {
                case 0:
                    break;
                case 1:
                    Client deleteClient = clientsGrid.SelectedItems[0] as Client;
                    db.Cars.RemoveRange(deleteClient.Cars);
                    db.Clients.Remove(deleteClient);
                    break;
                case 2:
                    Car deleteCar = carGrid.SelectedItems[0] as Car;
                    db.Cars.Remove(deleteCar);
                    break;
            }
            db.SaveChanges();
        }

        private void OnPrintClick(object sender, RoutedEventArgs e)
        {
            /*using (SqlConnection connection = new SqlConnection(connectionString))
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
                    string source = Environment.CurrentDirectory + "\\Print\\Замовлення №" + row["Id"] + ".docx";
                    try
                    {
                        File.Copy(Environment.CurrentDirectory + "\\Print\\Example.docx", source);
                    }
                    catch
                    {

                    }
                    doc = app.Documents.Open(source);
                    doc.Activate();

                    Word.Bookmarks wBookmarks = doc.Bookmarks;
                    Word.Range wRange;
                    sqlDataReader.Read();
                    wRange = wBookmarks[1].Range;
                    wRange.Text = sqlDataReader.GetValue(1).ToString() + " " + sqlDataReader.GetValue(2).ToString();
                    wRange = wBookmarks[2].Range;
                    wRange.Text = sqlDataReader.GetValue(5).ToString().Split(' ')[0];
                    wRange = wBookmarks[3].Range;
                    wRange.Text = sqlDataReader.GetValue(7).ToString();
                    wRange = wBookmarks[4].Range;
                    wRange.Text = sqlDataReader.GetValue(3).ToString();
                    wRange = wBookmarks[5].Range;
                    wRange.Text = sqlDataReader.GetValue(0).ToString();
                    wRange = wBookmarks[6].Range;
                    wRange.Text = sqlDataReader.GetValue(9).ToString();
                    wRange = wBookmarks[7].Range;
                    wRange.Text = sqlDataReader.GetValue(8).ToString();

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
            }*/
            
           
        }

        private void TextBoxGotFocus(object sender, RoutedEventArgs e)
        {
            if (TabPage1.IsSelected)
            {
                searchTextBox.Text = string.Empty;
            }
            else if (TabPage2.IsSelected)
            {
                SearchClientsTextBox.Text = string.Empty;
            }
            else
            {
                SearchCarsTextBox.Text = string.Empty;
            }
            
        }

        private void SearchPreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (SearchField.Text == "")
                return;
            int selectedIndex = tabControl1.SelectedIndex;
            switch (selectedIndex)
            {
                case 0:
                    break;
                case 1:
                    IEnumerable<Client> searchClients = null;
                    switch (SearchFieldClients.Text)
                    {   
                        case "Id":
                            searchClients =
                                db.Clients.Local.Where(
                                    attribute =>
                                    attribute.Id == Convert.ToInt32(SearchField.Text));
                        break;
                        case "Name":
                            searchClients =
                                db.Clients.Local.Where(
                                    attribute =>
                                    attribute.Name == SearchField.Text);
                            break;
                        case "Surname":
                            searchClients =
                                db.Clients.Local.Where(
                                    attribute =>
                                    attribute.Surname == SearchField.Text);
                            break;
                        case "MiddleName":
                            searchClients =
                                db.Clients.Local.Where(
                                    attribute =>
                                    attribute.MiddleName == SearchField.Text);
                            break;
                        case "PhoneNumber":
                            searchClients =
                                db.Clients.Local.Where(
                                    attribute =>
                                    attribute.PhoneNumber == SearchField.Text);
                            break;
                        case "Email":
                            searchClients =
                                db.Clients.Local.Where(
                                    attribute =>
                                    attribute.Email == SearchField.Text);
                            break;
                    }
                    clientsGrid.ItemsSource = searchClients;
                    break;
                case 2:
                    IEnumerable<Car> searchCars = null;
                    switch (SearchFieldCars.Text)
                    {
                        case "Id":
                            searchCars =
                                db.Cars.Local.Where(
                                    attribute =>
                                    attribute.Id == Convert.ToInt32(SearchField.Text));
                            break;
                        case "Model":
                            searchCars =
                                db.Cars.Local.Where(
                                    attribute =>
                                    attribute.Model == SearchField.Text);
                            break;
                        case "Variant":
                            searchCars =
                                db.Cars.Local.Where(
                                    attribute =>
                                    attribute.Variant == SearchField.Text);
                            break;
                        case "Type":
                            searchCars =
                                db.Cars.Local.Where(
                                    attribute =>
                                    attribute.Type == SearchField.Text);
                            break;
                        case "Year":
                            searchCars =
                                db.Cars.Local.Where(
                                    attribute =>
                                    attribute.Year == SearchField.Text);
                            break;
                        case "RegistrationPlate":
                            searchCars =
                                db.Cars.Local.Where(
                                    attribute =>
                                    attribute.RegistrationPlate == SearchField.Text);
                            break;
                        case "VIN":
                            searchCars =
                                db.Cars.Local.Where(
                                    attribute =>
                                    attribute.VIN == SearchField.Text);
                            break;
                    }
                    clientsGrid.ItemsSource = searchCars;
                    break;
            }
            /*if (searchTextBox.Text != string.Empty)
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        string sqlCommand;
                        if (TabPage1.IsSelected)
                        {
                            sqlCommand = "SELECT * FROM [Замовлення] WHERE [" + SearchField.Text + "] LIKE N'" + searchTextBox.Text + "%'";
                            SqlCommand command = new SqlCommand(sqlCommand, connection);
                            adapter = new SqlDataAdapter(command);
                            connection.Open();
                            ds.Clear();
                            adapter.Fill(ds);
                            ordersGrid.ItemsSource = ds.Tables[0].DefaultView;
                        }
                        else if (TabPage2.IsSelected)
                        {
                            sqlCommand = "SELECT * FROM [Клієнти] WHERE [" + SearchFieldClients.Text + "] LIKE N'" + SearchClientsTextBox.Text + "%'";
                            SqlCommand command = new SqlCommand(sqlCommand, connection);
                            adapter = new SqlDataAdapter(command);
                            connection.Open();
                            dsClient.Clear();
                            adapter.Fill(dsClient);
                            clientsGrid.ItemsSource = dsClient.Tables[0].DefaultView;
                        }
                        else
                        {
                            sqlCommand = "SELECT * FROM [Автомобілі] WHERE [" + SearchFieldCars.Text + "] LIKE N'" + SearchCarsTextBox.Text + "%'";
                            SqlCommand command = new SqlCommand(sqlCommand, connection);
                            adapter = new SqlDataAdapter(command);
                            connection.Open();
                            dsCar.Clear();
                            adapter.Fill(dsCar);
                            carGrid.ItemsSource = dsCar.Tables[0].DefaultView;
                        }
                        
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.Message, "Помилка!");
                    }
                }*/
        }

        private void MenuChangeStatusClick(object sender, RoutedEventArgs e)
        {
            /*DataRowView row = (DataRowView)ordersGrid.SelectedItems[0];
            string sqlExpression = "UPDATE  [Замовлення] SET [Статус] = N'Виконано' WHERE [Id] = " + row["Id"];

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                int amountAdd = command.ExecuteNonQuery();
                if (amountAdd == 0)
                    new MessageWindow("Не вдалося оновити", "Error").Show();
                connection.Close();
            }*/
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
        }

        private void OnAddClick(object sender, RoutedEventArgs e)
        {
            
            new AddOrderWindow().Show();
        }
    }
}
