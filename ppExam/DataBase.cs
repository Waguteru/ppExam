using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ppExam
{
    public class DataBase
    {

        NpgsqlConnection connection = new NpgsqlConnection("Server = localhost; port = 5432; Database = applicationvgtu;  User Id=postgres; Password= 123");

        public void OpenConnecting()
        {
            if(connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
        }

        public void CloseConnecting()
        {
            if(connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }

        public NpgsqlConnection GetConnection()
        {
            return connection;
        }
    }
}
