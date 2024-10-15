using System.Collections.Generic;
using System.Linq;
using TWSA.Models;

namespace TWSA.Data
{
    public class IssueRepository
    {
        private readonly ApplicationDbContext _context;

        public IssueRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddIssue(Issue issue)
        {
            _context.Issues.Add(issue);
            _context.SaveChanges();
        }

        public Issue GetIssue(int issueId)
        {
            return _context.Issues.FirstOrDefault(i => i.IssueId == issueId);
        }

        public List<Issue> GetAllIssues()
        {
            return _context.Issues.ToList();
        }

        public void UpdateIssue(Issue issue)
        {
            _context.Issues.Update(issue);
            _context.SaveChanges();
        }

        public void DeleteIssue(int issueId)
        {
            var issue = GetIssue(issueId);
            if (issue != null)
            {
                _context.Issues.Remove(issue);
                _context.SaveChanges();
            }
        }
    }
}