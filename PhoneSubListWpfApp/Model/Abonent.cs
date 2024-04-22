using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSubListWpfApp.Model
{
    public class Abonent
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string? Patronymic { get; set; }
        public int AddressId { get; set; }
        public int PhoneNumId { get; set; }
    
    }
}
