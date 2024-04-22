using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSubListWpfApp.ViewModel
{
    class AddressViewModel :INotifyPropertyChanged
    {
        private string _house;

        public string House
        {
            get => _house;
            private set
            {
                _house = value;
                NotifyPropertyChanged(nameof(House));
            }
        }



        public event PropertyChangedEventHandler? PropertyChanged;

        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged is null) return;
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
