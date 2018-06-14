using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
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
    /// Логика взаимодействия для RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        string connectionString;

        public RegisterWindow()
        {
            InitializeComponent();

            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        private void LoadImage(object sender, RoutedEventArgs e)
        {
            OpenFileDialog myDialog = new OpenFileDialog();
            myDialog.Filter = "Картинки(*.JPG;*.PNG)|*.JPG;*.PNG" + "|Все файлы (*.*)|*.* ";
            myDialog.CheckFileExists = true;
            myDialog.Multiselect = true;
            if (myDialog.ShowDialog() == true)
            {
                FileNameTextBlock.Text = myDialog.FileName;
            }
        }

        private void OnRegisterClick(object sender, RoutedEventArgs e)
        {
            string sqlExpression = "INSERT INTO [dbo].[Працівники] ([Ім’я], [Прізвище], [По батькові], [Посада]) VALUES " +
                "(N'" + NameTextBox.Text + "', N'" + SurnameTextBox.Text  + "', N'" + MiddleNameTextBox.Text + "', N'" + PositionTextBox.Text + "')";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                int amountAdd = command.ExecuteNonQuery();
                if (amountAdd > 0)
                    new MessageWindow("Додавання успішно виконано", "Warning").Show();
                else
                    new MessageWindow("Не вдалося додати", "Error").Show();
                connection.Close();
            }
        }
    }
}
