using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSubListWpfApp.ViewModel
{
    class StreetsViewModel : INotifyPropertyChanged
    {
        private string _city;

        public string City
        {
            get => _city;
            private set
            {
                _city = value;
                NotifyPropertyChanged(nameof(City));
            }
        }

        private string _street;

        public string Street
        {
            get => _street;
            private set
            {
                _street = value;
                NotifyPropertyChanged(nameof(Street));
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
