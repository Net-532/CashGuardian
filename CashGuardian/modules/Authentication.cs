using CashGuardian.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using CashGuardian;
using System;
using System.Data.SqlClient;
using CashGuardian.Models;
using CashGuardian.modules;
using System.Configuration;

namespace CashGuardian.Exceptions
{
    public class AuthenticationException : ApplicationException
    {
        public AuthenticationException(string message) : base(message) { }
    }
}


namespace CashGuardian.Services
{
    public class AuthenticationService
    {
        public User Authenticate(string username, string password)
        {
            User user = new User();

            try
            {
                using (var connection = DatabaseConnection.Instance.GetConnection())
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = "SELECT username FROM users WHERE username = @username AND password = @password";
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", password);

                    var result = command.ExecuteScalar().ToString();
                    if (result == null)
                    {
                        throw new AuthenticationException("Користувача не знайдено або пароль неправильний!");
                    }
                    user.Name = result;
                    return user;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }

    }

}
