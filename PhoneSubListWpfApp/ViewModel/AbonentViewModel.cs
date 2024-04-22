using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSubListWpfApp.ViewModel
{
    class AbonentViewModel:INotifyPropertyChanged
    {
        private string _firstName;

        public string FirstName
        {
            get => _firstName;
            private set
            {
                _firstName = value;
                NotifyPropertyChanged(nameof(FirstName));
            }
        }

        private string _secondName;

        public string SecondName
        {
            get => _secondName;
            private set
            {
                _secondName = value;
                NotifyPropertyChanged(nameof(SecondName));
            }
        }
        private string _patronymic;

        public string Patronymic
        {
            get => _patronymic;
            private set
            {
                _patronymic = value;
                NotifyPropertyChanged(nameof(Patronymic));
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
