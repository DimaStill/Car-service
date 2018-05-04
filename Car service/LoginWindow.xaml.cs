using System;
using System.Collections.Generic;
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
            if (TextBoxID.Text != String.Empty)
            {
                new MainWindow().Show();
                Close();
            }
            else
                MessageBox.Show("Введіть свій код", "Помилка!");
        }
    }
}
