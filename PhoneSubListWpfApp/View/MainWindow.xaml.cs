using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using CsvHelper;
using Dapper;
using Microsoft.Win32;
using PhoneSubListWpfApp.Backend.Helpers;
namespace PhoneSubListWpfApp.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DataContext _context;
        private IEnumerable<AbonentDto> _abonents;
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private async void  MainWindow_OnInitialized(object? sender, EventArgs e)
        {
            _context = new DataContext();

            await _context.Init();
            
            using var connection = _context.CreateConnection();
            connection.Open();
            _abonents = connection.Query<AbonentDto>("SELECT *" +
                                                     "FROM Abonent, PhoneNumber, Address, Streets " +
                                                     "where Abonent.PhoneNumId = PhoneNumber.Id AND Abonent.AddressId = Address.Id AND Streets.Id = Address.StreetId");
            
            ICollectionView abonents = CollectionViewSource.GetDefaultView(_abonents);

            dgAbonents.ItemsSource = abonents;


        }


        private void BtnSearchByPhoneOnClick(object sender, RoutedEventArgs e)
        {
            PhoneModuleWindow phoneModuleWindow = new PhoneModuleWindow();
            if (phoneModuleWindow.ShowDialog() == true)
            {
                var phoneNum = phoneModuleWindow.PhoneNum;
                if (!string.IsNullOrWhiteSpace(phoneNum))
                {
                    var filteredAbonents = _abonents.Where(x=>x.PhoneNum.StartsWith(phoneNum) 
                                                              || x.HomePhoneNum.StartsWith(phoneNum)
                                                              || x.WorkPhoneNum.StartsWith(phoneNum)).ToArray();
                    dgAbonents.ItemsSource = filteredAbonents;
                    if (filteredAbonents.Length == 0)
                    {
                        tbxNoResult.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        tbxNoResult.Visibility = Visibility.Hidden;
                    }
                }
                else
                {
                    dgAbonents.ItemsSource = _abonents;
                    tbxNoResult.Visibility = Visibility.Hidden;
                }
            }
        }

        private void BtnStreetsListOnClick(object sender, RoutedEventArgs e)
        {
            StreetsModuleWindow streetsModuleWindow = new StreetsModuleWindow();
            streetsModuleWindow.ShowDialog();
        }

        private void BtnExportIntoCsvOnClick(object sender, RoutedEventArgs e)
        {
            var dlg = new SaveFileDialog();
            dlg.FileName = $"report_{DateTime.Now.ToString("yyyyMMddHHmmssfff")}";
            dlg.DefaultExt = ".csv";
            if (dlg.ShowDialog() == true)
            {
                using (var writer = new StreamWriter($"{dlg.FileName}.csv"))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecords(dgAbonents.ItemsSource);
                }
            }
        }
        private void TbxFilterOnTextChanged(object sender, TextChangedEventArgs e)
        {
            var filteredAbonents = _abonents;
            if (!string.IsNullOrWhiteSpace(tbxFirstName.Text))
                filteredAbonents = filteredAbonents.Where(x => x.FirstName.Contains(tbxFirstName.Text, StringComparison.InvariantCultureIgnoreCase));
            if (!string.IsNullOrWhiteSpace(tbxSecondName.Text))
                filteredAbonents = filteredAbonents.Where(x => x.FirstName.Contains(tbxSecondName.Text, StringComparison.InvariantCultureIgnoreCase));
            if (!string.IsNullOrWhiteSpace(tbxPatronymic.Text))
                filteredAbonents = filteredAbonents.Where(x => x.FirstName.Contains(tbxPatronymic.Text, StringComparison.InvariantCultureIgnoreCase));
            if (!string.IsNullOrWhiteSpace(tbxStreet.Text))
                filteredAbonents = filteredAbonents.Where(x => x.FirstName.Contains(tbxStreet.Text, StringComparison.InvariantCultureIgnoreCase));
            if (!string.IsNullOrWhiteSpace(tbxHouse.Text))
                filteredAbonents = filteredAbonents.Where(x => x.FirstName.Contains(tbxHouse.Text, StringComparison.InvariantCultureIgnoreCase));
            if (!string.IsNullOrWhiteSpace(tbxPhoneNum.Text))
                filteredAbonents = filteredAbonents.Where(x => x.FirstName.Contains(tbxPhoneNum.Text, StringComparison.InvariantCultureIgnoreCase));
            if (!string.IsNullOrWhiteSpace(tbxHomePhoneNum.Text))
                filteredAbonents = filteredAbonents.Where(x => x.FirstName.Contains(tbxHomePhoneNum.Text, StringComparison.InvariantCultureIgnoreCase));
            if (!string.IsNullOrWhiteSpace(tbxWorkPhoneNum.Text))
                filteredAbonents = filteredAbonents.Where(x => x.FirstName.Contains(tbxWorkPhoneNum.Text, StringComparison.InvariantCultureIgnoreCase));


            dgAbonents.ItemsSource = filteredAbonents;
        }
        void DgAbonentsAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            var desc = e.PropertyDescriptor as PropertyDescriptor;
            var att = desc.Attributes[typeof(ColumnNameAttribute)] as ColumnNameAttribute;
            if (att != null)
            {
                e.Column.Header = att.Name;
            }
        }
        private class AbonentDto
        {
            [ColumnName("Имя")]
            public string FirstName { get; set; }
            [ColumnName("Фамилия")]
            public string SecondName { get; set; }
            [ColumnName("Отчество")]
            public string? Patronymic { get; set; }
            [ColumnName("Улица")]
            public string Street { get; set; }
            [ColumnName("Дом")]
            public string House { get; set; }
            [ColumnName("Мобильный номер")]
            public string PhoneNum { get; set; }
            [ColumnName("Домашний номер")]
            public string HomePhoneNum { get; set; }
            [ColumnName("Рабочий номер")]
            public string WorkPhoneNum { get; set; }

        }
    }
}