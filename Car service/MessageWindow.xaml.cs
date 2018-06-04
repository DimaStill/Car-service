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
using System.Windows.Shapes;

namespace Car_service
{
    /// <summary>
    /// Логика взаимодействия для MessageWindow.xaml
    /// </summary>
    public partial class MessageWindow : Window
    {
        public MessageWindow()
        {
            InitializeComponent();
        }

        public MessageWindow(string text, string typeMessage)
        {
            InitializeComponent();

            textMessage.Text = text;

            switch (typeMessage)
            {
                case "Error":
                    typeMessageImage.Source = BitmapFrame.Create(new Uri("pack://application:,,,/Resources/error_Icon.png"));
                    Title = "Помилка";
                    break;
                case "Warning":
                    typeMessageImage.Source = BitmapFrame.Create(new Uri("pack://application:,,,/Resources/warning_Icon.png"));
                    Title = "Повідомлення";
                    break;
            }
        }

        private void OnOK(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
