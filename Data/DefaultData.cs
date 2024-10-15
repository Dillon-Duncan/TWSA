using TWSA.Models;

namespace TWSA.Data
{
    public static class DefaultData
    {
        public static List<User> GetDefaultUsers()
        {
            return new List<User>
            {
                new User { UserId = 1, Username = "johndoe", Password = "password123", FirstName = "John", Surname = "Doe", Age = 30, City = "Cape Town", Email = "johndoe@example.com", PhoneNumber = "0821234567", IsAdmin = false },
                new User { UserId = 2, Username = "janedoe", Password = "password123", FirstName = "Jane", Surname = "Doe", Age = 28, City = "Johannesburg", Email = "janedoe@example.com", PhoneNumber = "0831234567", IsAdmin = false },
                new User { UserId = 3, Username = "admin", Password = "adminpassword", FirstName = "Admin", Surname = "User", Age = 35, City = "Pretoria", Email = "admin@example.com", PhoneNumber = "0841234567", IsAdmin = true }
            };
        }

        public static List<Issue> GetDefaultIssues()
        {
            return new List<Issue>
            {
                new Issue { IssueId = 1, UserId = 1, Subject = "Water Leakage", Location = "Main Street, Cape Town", Category = "Water", Description = "There is a major water leakage near Main Street.", ReportDateTime = DateTime.Now.AddDays(-3) },
                new Issue { IssueId = 2, UserId = 2, Subject = "Power Outage", Location = "Downtown, Johannesburg", Category = "Electricity", Description = "Power has been out for 5 hours.", ReportDateTime = DateTime.Now.AddDays(-1) }
            };
        }

        public static List<EventAnnouncement> GetDefaultEvents()
        {
            return new List<EventAnnouncement>
            {
                new EventAnnouncement { EventId = 1, IsEvent = true, Subject = "Community Clean-Up", Description = "Join us for a community clean-up event.", Category = "Community", Location = "Green Point Park, Cape Town", EventDateTime = DateTime.Now.AddDays(5), MediaAttachment = "cleanup.jpg" },
                new EventAnnouncement { EventId = 2, IsEvent = true, Subject = "Charity Run", Description = "Participate in our charity run to raise funds.", Category = "Charity", Location = "Sandton, Johannesburg", EventDateTime = DateTime.Now.AddDays(10), MediaAttachment = "charityrun.jpg" }
            };
        }
    }
}