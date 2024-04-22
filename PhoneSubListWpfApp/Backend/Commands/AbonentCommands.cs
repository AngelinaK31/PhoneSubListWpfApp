using Dapper;
using PhoneSubListWpfApp.Model;
using PhoneSubListWpfApp.Backend.Helpers;

namespace PhoneSubListWpfApp.Backend.Commands
{
    class AbonentCommands
    {
        public static List<Abonent> LoadAbonents()
        {
            var context = new DataContext();
            using var connection = context.CreateConnection();
            
            var output = connection.Query<Abonent>("SELECT * FROM Person", new DynamicParameters());
            return output.ToList();
            
        }

    }
}
