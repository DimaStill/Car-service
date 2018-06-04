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
        string connectionString;

        public AddOrderWindow()
        {
            InitializeComponent();

            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        private void OnAddClick(object sender, RoutedEventArgs e)
        {
            string sqlExpression = "INSERT INTO [dbo].[Замовлення] " +
                "([Марка], [Модель], [Державний номер], [VIN], [Дата], [Статус], [Механік], [Власник], [Роботи], [Вартість]) " +
                "VALUES (N'" + modelTextBox.Text + "', N'" + variantTextBox.Text + "', N'" + govNumber.Text + "', N'" + 
                VINTextBox.Text + "', N'" + DateTime.Now + "', N'Не виконано', N'Вірскій Олексій Генадійович', N'" + 
                surnameTextBox.Text + " " + nameTextBox.Text + " " + middleNameTextBox.Text;
            int sum = 0;
            if ((bool)CheckBox1.IsChecked)
            {
                sqlExpression += "', N'Ремонт ДВЗ, ";
                sum += 100;
            }
            if ((bool)CheckBox2.IsChecked)
            {
                sqlExpression += "', N'Ремонт ходової частини, ";
                sum += 200;
            }
            if ((bool)CheckBox3.IsChecked)
            {
                sqlExpression += "', N'Проточка гальмівних дисків на стенді, ";
                sum += 200;
            }
            if ((bool)CheckBox4.IsChecked)
            {    sqlExpression += "', N'Реставрація рульових рейок, ";
                sum += 300;
            }
            if ((bool)CheckBox5.IsChecked)
            {
                sqlExpression += "', N'Ремонт КПП і АКПП, ";
                sum += 300;
            }
            if ((bool)CheckBox6.IsChecked)
            {
                sqlExpression += "', N'Заправка і обслуговування автокондиціонерів, ";
                sum += 300;
            }
            if ((bool)CheckBox7.IsChecked)
            {
                sqlExpression += "', N'Чистка паливних форсунок і дросельних заслінок, ";
                sum += 400;
            }
            if ((bool)CheckBox8.IsChecked)
            { 
                sqlExpression += "', N'Ремонт автоелектрики будь-якої складності, ";
                sum += 400;
            }
            if ((bool)CheckBox9.IsChecked)
            { 
                sqlExpression += "', N'Встановлення додаткового обладнання, ";
                sum += 400;
            }
            if ((bool)CheckBox10.IsChecked)
            {
                sqlExpression += "', N'Встановлення механічної протиугінної системи, ";
                sum += 400;
            }
            if ((bool)CheckBox11.IsChecked)
            {
                sqlExpression += "', N'Комп'ютерна діагностика всіх марок і систем автомобілів, ";
                sum += 500;
            }
            if ((bool)CheckBox11.IsChecked)
            {
                sqlExpression += "', N'Регулювання фар на стенді, ";
                sum += 500;
            }
            sqlExpression += "', " + sum + ")";

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
