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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Car_service
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        string connectionString;

        public LoginWindow()
        {
            InitializeComponent();

            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        private void LoginPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key < Key.D0 && e.Key > Key.D9)
                e.Handled = true;
        }

        private void OnLoginClick(object sender, RoutedEventArgs e)
        {
            string sqlCommand = $"SELECT * FROM [Працівники] WHERE [ID] = {TextBoxID.Text}";
            SqlConnection connection = null;
            try
            {
                if (TextBoxID.Text == String.Empty)
                    throw new Exception("Введіть свій унікальний код");
                User user = new User();
                using (connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlCommand, connection);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        reader.Read();
                        if (!reader.HasRows)
                            throw new Exception("Робітник з таким кодом не знайдено");
                        user.ConvertToUser(reader);
                    }
                }
                new MainWindow(user).Show();
                Close();
            }
            catch (Exception exception)
            {
                new MessageWindow(exception.Message, "Error").Show();
            }
        }
    }
}
