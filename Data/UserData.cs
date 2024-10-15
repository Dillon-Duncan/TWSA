using System.Collections.Generic;
using TWSA.Models;

namespace TWSA.Data
{
    public static class UserData
    {
        private static readonly List<User> users = new List<User>();

        public static void AddUser(User user)
        {
            // Ensure password is hashed before storing
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            users.Add(user);
        }

        public static List<User> GetAllUsers()
        {
            return new List<User>(users);
        }

        public static User GetUser(string username)
        {
            return users.Find(u => u.Username == username);
        }

        public static bool ValidateUser(string username, string password)
        {
            var user = GetUser(username);
            return user != null && BCrypt.Net.BCrypt.Verify(password, user.Password);
        }

        public static void UpdateUser(User user)
        {
            var existingUser = GetUser(user.Username);
            if (existingUser != null)
            {
                users.Remove(existingUser);
                users.Add(user);
            }
        }

        public static void DeleteUser(string username)
        {
            var user = GetUser(username);
            if (user != null)
            {
                users.Remove(user);
            }
        }
    }
}