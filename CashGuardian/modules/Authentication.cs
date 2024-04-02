using CashGuardian.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using CashGuardian;
using System;
using System.Xml.Linq;

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
        static private List<User> users = new List<User>()
        {
        // Initial users - in a real application, these would be managed in a database
        new User() { Username = "user1", Name = "John Doe", Password = "password123" },
        new User() { Username = "user2", Name = "Jane Doe", Password = "password456" }
        };

        static public User Authenticate(string username, string password)
        {
            bool userExists = users.Where(u => u.Username == username && u.Password == password).Any();

            if (!userExists)
            {
                throw new AuthenticationException("Користувача не знайдено або пароль неправильний!");
            }

            var user = users.First(u => u.Username == username);

            return user;
        }

    }

}
