using Microsoft.AspNetCore.Mvc;
using TWSA.Data;
using TWSA.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace TWSA.Controllers
{
    public class IssueController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IssueController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Displays the form to report an issue
        [HttpGet]
        public IActionResult ReportIssue()
        {
            if (HttpContext.Session.GetString("LoggedInUser") == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        // Handles the issue reporting form submission
        [HttpPost]
        public async Task<IActionResult> ReportIssue(Issue issue)
        {
            if (HttpContext.Session.GetString("LoggedInUser") == null)
            {
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {
                issue.ReportDateTime = DateTime.Now;
                issue.UserId = (await _context.Users.FirstOrDefaultAsync(u => u.Username == HttpContext.Session.GetString("LoggedInUser"))).UserId;
                _context.Issues.Add(issue);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Issue reported successfully!";
                return RedirectToAction("ViewReports");
            }
            return View(issue);
        }

        // Displays a list of reported issues
        [HttpGet]
        public async Task<IActionResult> ViewReports()
        {
            if (HttpContext.Session.GetString("IsAdmin") != "True")
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            var issues = await _context.Issues.Include(i => i.UserId).ToListAsync();
            return View(issues);
        }
    }
}
