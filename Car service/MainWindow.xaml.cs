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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public MainWindow(User currentUser)
        {
            InitializeComponent();
            DataContext = currentUser;
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
    }
}
