using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSubListWpfApp.ViewModel
{
    class PhoneNumberViewModel :INotifyPropertyChanged
    {
        private string _phoneNum;

        public string PhoneNum
        {
            get => _phoneNum;
            private set
            {
                _phoneNum = value;
                NotifyPropertyChanged(nameof(PhoneNum));
            }
        }

        private string _homePhoneNum;

        public string HomePhoneNum
        {
            get => _homePhoneNum;
            private set
            {
                _homePhoneNum = value;
                NotifyPropertyChanged(nameof(HomePhoneNum));
            }
        }

        private string _workPhoneNum;

        public string WorkPhoneNum
        {
            get => _workPhoneNum;
            private set
            {
                _workPhoneNum = value;
                NotifyPropertyChanged(nameof(WorkPhoneNum));
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
