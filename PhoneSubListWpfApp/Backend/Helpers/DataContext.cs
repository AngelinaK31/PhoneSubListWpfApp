using System.Data;
using Dapper;
using System.Data.SQLite;
using Microsoft.Extensions.Configuration;

namespace PhoneSubListWpfApp.Backend.Helpers
{
    public class DataContext
    {
        public IDbConnection CreateConnection()
        {
            return new SQLiteConnection("Data Source=InMemory;Mode=Memory;Cache=Shared");
        }

        public async Task Init()
        {
            using var connection = CreateConnection();
                var sql = """
                             
                              CREATE TABLE IF NOT EXISTS
                              Abonent (
                                  Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                                  FirstName TEXT,
                                  SecondName TEXT,
                                  Patronymic TEXT,
                                  AddressId INTEGER,
                                  PhoneNumId INTEGER,
                                  FOREIGN KEY (AddressId)  REFERENCES Address (Id),
                                  FOREIGN KEY (PhoneNumId)  REFERENCES PhoneNumber (Id)
                              );
                              
                              CREATE TABLE IF NOT EXISTS
                              Streets (
                                  Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                                  City TEXT,
                                  Street TEXT
                              );
                              
                              CREATE TABLE IF NOT EXISTS
                              PhoneNumber (
                                  Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                                  PhoneNum TEXT,
                                  HomePhoneNum TEXT,
                                  WorkPhoneNum TEXT
                              );
                              
                               CREATE TABLE IF NOT EXISTS
                              Address (
                                  Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                                  House TEXT,
                                  StreetId INTEGER,
                                  FOREIGN KEY (StreetId)  REFERENCES Streets (Id)
                              );
                              
                              INSERT INTO Streets (City,Street) 
                              Values
                              ('Краснодар', 'Красная'),
                              ('Краснодар', 'Северная'),
                              ('Краснодар', 'Коммунаров'),
                              ('Краснодар', 'Тюляева'),
                              ('Краснодар', 'Бабушкина');

                               INSERT INTO PhoneNumber (PhoneNum,HomePhoneNum,WorkPhoneNum) 
                              Values
                              ('+7(999)9999999', '+7(999)5533555','+7(999)9955656'),
                              ('+7(999)8888888', '+7(999)4562356','+7(999)1256896'),
                              ('+7(999)7777777', '+7(999)1596347','+7(999)6685911'),
                              ('+7(999)6666666', '+7(999)6648752','+7(999)3226159'),
                              ('+7(999)5555555', '+7(999)1524245','+7(999)3265263');

                               INSERT INTO Address (StreetId,House) 
                              Values
                              (5,'25A'),
                              (3,'125'),
                              (1,'1'),
                              (1,'36'),
                              (4,'256');
                                 INSERT INTO Abonent (FirstName,SecondName,Patronymic, AddressId,PhoneNumId) 
                             Values
                              ('Иван', 'Иванов', 'Иванович',1,1),
                              ('Мария', 'Тестова', 'Тестовна',2,2),
                              ('Дмитрий', 'Шошин', 'Михайлович',3,3),
                              ('Наталья', 'Митягина', 'Павловна',4,4),
                              ('Ангелина', 'Калинина', 'Евгеньевна',5,5);
                                
                             """;
                await connection.ExecuteAsync(sql);
            
        }
    }
}
