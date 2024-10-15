using Microsoft.AspNetCore.Mvc;
using TWSA.Data;
using TWSA.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

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
                try
                {
                    issue.ReportDateTime = DateTime.Now;
                    var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == HttpContext.Session.GetString("LoggedInUser"));
                    if (user == null)
                    {
                        return RedirectToAction("Login", "Account");
                    }
                    issue.UserId = user.UserId;
                    _context.Issues.Add(issue);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Issue reported successfully!";
                    return RedirectToAction("ViewReports");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "An error occurred while reporting the issue. Please try again.");
                    return View(issue);
                }
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

            try
            {
                var issues = await _context.Issues.Include(i => i.User).ToListAsync();
                return View(issues);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while retrieving reports. Please try again.";
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            if (HttpContext.Session.GetString("IsAdmin") != "True")
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            try
            {
                var issue = await _context.Issues.Include(i => i.User).FirstOrDefaultAsync(i => i.IssueId == id);
                if (issue == null)
                {
                    return NotFound();
                }
                return View(issue);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while retrieving issue details. Please try again.";
                return RedirectToAction("ViewReports");
            }
        }
    }
}
