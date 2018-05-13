using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_service
{
    public class User
    {
        private int id;
        private string name;
        private string surname;
        private string middleName;
        private string position;

        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string MiddleName { get; set; }
        public string Position { get; set; }

        public void ConvertToUser(SqlDataReader reader)
        {
            ID = Convert.ToInt32(reader.GetValue(0));
            Name = reader.GetValue(1).ToString();
            Surname = reader.GetValue(2).ToString();
            MiddleName = reader.GetValue(3).ToString();
            Position = reader.GetValue(4).ToString();
        }
    }
}
