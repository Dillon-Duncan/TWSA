using Newtonsoft.Json;
using TWSA.Helpers;
using TWSA.Models;

namespace TWSA.Data
{
    public static class UserData
    {
        private static readonly List<User> users = new List<User>();
        private static readonly string filePath = "Data/users.txt";

        static UserData()
        {
            LoadData();
            if (users.Count == 0) // If no data, load default data
            {
                users.AddRange(DefaultData.GetDefaultUsers());
                SaveData();
            }
        }

        public static void AddUser(User user)
        {
            // Ensure password is hashed before storing
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            users.Add(user);
            SaveData();
        }

        public static User GetUser(string username)
        {
            return users.Find(u => u.Username == username)!;
        }

        public static bool ValidateUser(string username, string password)
        {
            var user = GetUser(username);
            return user != null && BCrypt.Net.BCrypt.Verify(password, user.Password);
        }

        public static List<User> GetAllUsers()
        {
            return new List<User>(users);
        }

        private static void SaveData()
        {
            var jsonData = JsonConvert.SerializeObject(users);
            var encryptedData = EncryptionHelper.Encrypt(jsonData);
            File.WriteAllText(filePath, encryptedData);
        }

        private static void LoadData()
        {
            if (File.Exists(filePath))
            {
                var encryptedData = File.ReadAllText(filePath);
                var jsonData = EncryptionHelper.Decrypt(encryptedData);
                var loadedUsers = JsonConvert.DeserializeObject<List<User>>(jsonData);
                if (loadedUsers != null)
                {
                    users.AddRange(loadedUsers);
                }
            }
        }
    }
}