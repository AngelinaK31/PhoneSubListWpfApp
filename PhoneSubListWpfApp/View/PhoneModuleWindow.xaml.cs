
using System.Windows;

namespace PhoneSubListWpfApp.View
{
    /// <summary>
    /// Interaction logic for PhoneModuleWindow.xaml
    /// </summary>
    public partial class PhoneModuleWindow : Window
    {
        public PhoneModuleWindow()
        {
            InitializeComponent();
        }

        private void btnAcceptOnClick(object sender, RoutedEventArgs e)
        {

            this.DialogResult = true;
        }

        public string PhoneNum
        {
            get { return tbxSearchPhoneNum.Text; }
        }
    }
}
