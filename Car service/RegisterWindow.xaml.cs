using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
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
        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void LoadImage(object sender, RoutedEventArgs e)
        {
            /*OpenFileDialog myDialog = new OpenFileDialog();
            myDialog.Filter = "Картинки(*.JPG;*.PNG)|*.JPG;*.PNG" + "|Все файлы (*.*)|*.* ";
            myDialog.CheckFileExists = true;
            myDialog.Multiselect = true;
            if (myDialog.ShowDialog() == true)
            {
                string id = "select top 1 * from [dbo].[Працівники] order by Id desc";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    
                    SqlCommand commandId = new SqlCommand(id, connection);
                    using (SqlDataReader reader = commandId.ExecuteReader())
                    {
                        reader.Read();
                        id = reader.GetValue(0).ToString();
                    }
                    connection.Close();
                }
                FileNameTextBlock.Text = myDialog.FileName;
                string format = myDialog.FileName.Split('.')[1];
                File.Copy(myDialog.FileName, "Resources/" + id + "." + format);
                System.Windows.Resources.StreamResourceInfo res =
                    Application.GetResourceStream(new Uri("images/river.jpg", UriKind.Relative));
            }*/
        }

        private void OnRegisterClick(object sender, RoutedEventArgs e)
        {
            User newUser = new User
            {
                Name = NameTextBox.Text,
                Surname = SurnameTextBox.Text,
                MiddleName = MiddleNameTextBox.Text,
                Position = PositionTextBox.Text
            };

            using (CarServiceContext db = new CarServiceContext())
            {
                db.Users.Add(newUser);
                db.SaveChanges();
            }

            Close();
        }
    }
}
