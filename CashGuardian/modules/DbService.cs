using System;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace CashGuardian.modules
{
    public class DatabaseConnection
    {
        private static DatabaseConnection instance = null;
        private SqlConnection connection;
        private string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=cashguard_db;Integrated Security=True;Connect Timeout=30;Encrypt=False";

        private DatabaseConnection()
        {
            connection = new SqlConnection(connectionString);
        }

        public static DatabaseConnection Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DatabaseConnection();
                }
                return instance;
            }
        }

        public SqlConnection GetConnection()
        {
            return connection;
        }
    }

    class DbService
    {
        public void CreateUsers(List<(string username, string password)> users)
        {
            using (var connection = DatabaseConnection.Instance.GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();

                // Видалення всіх користувачів
                command.CommandText = "DELETE FROM users";
                command.ExecuteNonQuery();

                // Додавання користувачів з переданого списку
                command.CommandText = "INSERT INTO users (username, password) VALUES (@username, @password)";
                foreach (var user in users)
                {
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@username", user.username);
                    command.Parameters.AddWithValue("@password", user.password);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
