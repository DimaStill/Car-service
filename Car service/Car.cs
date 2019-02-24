using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Car_service
{
    public class Car : INotifyPropertyChanged
    {
        public int Id
        {
            get { return Id; }
            set { OnPropertyChanged("Id"); }
        }
        public string Model
        {
            get { return Model; }
            set { OnPropertyChanged("Model"); }
        }
        public string Variant
        {
            get { return Variant; }
            set { OnPropertyChanged("Variant"); }
        }
        public string Type
        {
            get { return Type; }
            set { OnPropertyChanged("Type"); }
        }
        public string Year
        {
            get { return Year; }
            set { OnPropertyChanged("Year"); }
        }
        public string RegistrationPlate
        {
            get { return RegistrationPlate; }
            set { OnPropertyChanged("RegistrationPlate"); }
        }
        public string VIN
        {
            get { return VIN; }
            set { OnPropertyChanged("VIN"); }
        }

        public int ClientId { get; set; }
        public Client Owner { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
