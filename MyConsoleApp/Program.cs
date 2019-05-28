using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;


namespace MyConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {

            SQLiteConnection sqlConnection = new SQLiteConnection();
            sqlConnection.ConnectionString = "data source=C:\\Users\\Jack Fuller\\Desktop\\SQLiteCourse\\Test.db";
            sqlConnection.Open();

            //Fill SQL Command Parameters
            SQLiteCommand sqlCommand = new SQLiteCommand();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandType = System.Data.CommandType.Text;

            //Select Field1, Field2, Field3
            //From TableName
            //Where Conditions
            //Select * - gets all fields
            sqlCommand.CommandText = "Select * From UK";           
            
            SQLiteDataReader dataReader = sqlCommand.ExecuteReader();

            List<string> emailAddresses = new List<string>();           

            while (dataReader.Read())
            {
                string fullemailAddress = dataReader.GetString(10);
                string emailDomain = fullemailAddress.Substring(fullemailAddress.IndexOf("@") + 1);
                string emailDomainPerfected = emailDomain.Substring(0, emailDomain.IndexOf("."));
                emailAddresses.Add(emailDomainPerfected);

                //Console.WriteLine("ID: {0},FirstName: {1},LastName:  {2}", dataReader.GetInt16(0), dataReader.GetString(1), dataReader.GetString(2));
            }

            Dictionary<string, int> frequencies = GetFrequencies(emailAddresses);
            DisplaySortedFrequencies(frequencies);

            Console.ReadKey();
        }

        static Dictionary<string, int> GetFrequencies(List<string> addresses)
        {
            Dictionary<string, int> result = new Dictionary<string, int>();

            foreach (string value in addresses)
            {
                if (result.TryGetValue(value, out int count))
                {
                    result[value] = count + 1;
                }
                else
                {
                    result.Add(value, 1);
                } 
            }

            return result;
        }

        static void DisplaySortedFrequencies(Dictionary<string, int> frequencies)
        {
            var sorted = from pair in frequencies
                         orderby pair.Value descending
                         select pair;

            foreach(var pair in sorted)
            {
                Console.WriteLine($"{pair.Key} = { pair.Value}");
            }
        }
    }    
}
