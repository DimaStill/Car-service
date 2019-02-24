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
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key < Key.D0 && e.Key > Key.D9)
                e.Handled = true;
        }

        private void OnLoginClick(object sender, RoutedEventArgs e)
        {
            if (TextBoxID.Text == string.Empty)
                new MessageWindow("Введіть свій унікальний код", "Error").Show();

            User loginUser;
            int inputLogin = Convert.ToInt32(TextBoxID.Text);
            using (CarServiceContext db = new CarServiceContext())
                loginUser = db.Users.FirstOrDefault(user => user.Id == inputLogin);
            if (loginUser != null)
            {
                new MainWindow(loginUser).Show();
                Close();
            }
            else
                new MessageWindow("Робітника з таким кодом не знайдено", "Error").Show();
        }

        private void OnRegisterMouseDown(object sender, MouseButtonEventArgs e)
        {
            new RegisterWindow().Show();
        }
    }
}
