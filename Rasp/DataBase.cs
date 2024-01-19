using MySql.Data;
using MySql.Data.MySqlClient;
using System;

namespace Rasp
{
    class DataBase : IDisposable
    {
        private MySqlConnection _connection;
        private string host = "server6.hosting.reg.ru";
        private string port = "3306";
        private string username = "u0941340_kea";
        private string password = "vJ1gP4hZ9evA8zC7";
        private string database = "u0941340_kea";

        public DataBase() 
        { 
            _connection = new MySqlConnection($"Server={host};Database={database};port={port};User Id={username};password={password}");
        }

        public void OpenConnection()
        {
            _connection.Open();
        }

        public MySqlConnection GetConnection() => _connection;

        public void CloseConnection()
        {
            _connection.Close();
        }

        public void Dispose()
        {
            CloseConnection();
        }
    }
}
