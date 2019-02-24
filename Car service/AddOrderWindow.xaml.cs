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
    /// Логика взаимодействия для AddOrderWindow.xaml
    /// </summary>
    public partial class AddOrderWindow : Window
    {
        Random random = new Random();

        string[] typeCar = { "Легковий", "Електромобіль", "Мотоцикл", "Мікроавтобус", "Автобус", "Вантажний", "Фура" };

        public AddOrderWindow()
        {
            InitializeComponent();
        }

        private void OnAddClick(object sender, RoutedEventArgs e)
        {
                Client newClient = new Client
                {
                    Name = nameTextBox.Text,
                    Surname = surnameTextBox.Text,
                    MiddleName = middleNameTextBox.Text,
                    PhoneNumber = phoneTextBox.Text,
                    Email = emailTextBox.Text
                };
                MainWindow.db.Clients.Add(newClient);
                MainWindow.db.SaveChanges();

                Car newCar = new Car
                {
                    Model = modelTextBox.Text,
                    Variant = variantTextBox.Text,
                    Type = typeCar[typeVehicleComboBox.SelectedIndex],
                    Year = yearTextBox.Text,
                    RegistrationPlate = registrationPlate.Text,
                    VIN = VINTextBox.Text,
                    Owner = newClient
                };
                MainWindow.db.Cars.Add(newCar);
                MainWindow.db.SaveChanges();
        }
    }
}
