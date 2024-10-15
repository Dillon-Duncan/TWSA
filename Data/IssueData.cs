using System.Collections.Generic;
using TWSA.Models;

namespace TWSA.Data
{
    public static class IssueData
    {
        private static readonly List<Issue> issues = new List<Issue>();

        public static void AddIssue(Issue issue)
        {
            issues.Add(issue);
        }

        public static List<Issue> GetAllIssues()
        {
            return new List<Issue>(issues);
        }

        public static Issue GetIssue(int issueId)
        {
            return issues.Find(i => i.IssueId == issueId);
        }

        public static void UpdateIssue(Issue issue)
        {
            var existingIssue = GetIssue(issue.IssueId);
            if (existingIssue != null)
            {
                issues.Remove(existingIssue);
                issues.Add(issue);
            }
        }

        public static void DeleteIssue(int issueId)
        {
            var issue = GetIssue(issueId);
            if (issue != null)
            {
                issues.Remove(issue);
            }
        }
    }
}