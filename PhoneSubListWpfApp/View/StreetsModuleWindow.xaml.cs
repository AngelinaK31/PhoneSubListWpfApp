using System.Windows;
using System.Windows.Controls;
using Dapper;
using System.ComponentModel;
using PhoneSubListWpfApp.Backend.Helpers;

namespace PhoneSubListWpfApp.View
{
    /// <summary>
    /// Interaction logic for StreetsModuleWindow.xaml
    /// </summary>
    public partial class StreetsModuleWindow : Window
    {
        public StreetsModuleWindow()
        {
            InitializeComponent();

            var context = new DataContext();

            using var connection = context.CreateConnection();
            connection.Open();
            var streets = connection.Query<StreetsDto>("SELECT Streets.Street, Streets.City, COUNT(Abonent.Id) as AbonentCount " +
                                                     "FROM Streets " +
                                                     "JOIN Address on Address.StreetId = Streets.Id " +
                                                     "Left JOIN Abonent on Abonent.AddressId = Address.Id " +
                                                     "GROUP BY Streets.Id ");
            dgStreets.ItemsSource = streets;
        }

        void dgStreetsAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            var desc = e.PropertyDescriptor as PropertyDescriptor;
            var att = desc.Attributes[typeof(ColumnNameAttribute)] as ColumnNameAttribute;
            if (att != null)
            {
                e.Column.Header = att.Name;
            }
        }

        private class StreetsDto
        {
            [ColumnName("Город")]
            public string City {get; set; }
            [ColumnName("Улица")]
            public string Street { get; set; }
            [ColumnName("Кол-во абонентов")]
            public int AbonentCount { get; set; }
        }
    }
}
