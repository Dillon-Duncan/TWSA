using Newtonsoft.Json;
using TWSA.Helpers;
using TWSA.Models;

namespace TWSA.Data
{
    public static class IssueData
    {
        private static readonly List<Issue> issues = new List<Issue>();
        private static readonly string filePath = "Data/issues.txt";

        static IssueData()
        {
            LoadData();
            if (issues.Count == 0) // If no data, load default data
            {
                issues.AddRange(DefaultData.GetDefaultIssues());
                SaveData();
            }
        }

        public static void AddIssue(Issue issue, string username)
        {
            issue.UserId = UserData.GetUser(username).UserId;
            issues.Add(issue);
            SaveData();
        }

        public static List<Issue> GetAllIssues()
        {
            return new List<Issue>(issues);
        }

        private static void SaveData()
        {
            var jsonData = JsonConvert.SerializeObject(issues);
            var encryptedData = EncryptionHelper.Encrypt(jsonData);
            File.WriteAllText(filePath, encryptedData);
        }

        private static void LoadData()
        {
            if (File.Exists(filePath))
            {
                var encryptedData = File.ReadAllText(filePath);
                var jsonData = EncryptionHelper.Decrypt(encryptedData);
                var loadedIssues = JsonConvert.DeserializeObject<List<Issue>>(jsonData);
                if (loadedIssues != null)
                {
                    issues.AddRange(loadedIssues);
                }
            }
        }
    }
}