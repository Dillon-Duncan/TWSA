using Microsoft.AspNetCore.Mvc;
using TWSA.Data;
using TWSA.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Linq;

namespace TWSA.Controllers
{
    public class IssueController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<IssueController> _logger;

        public IssueController(ApplicationDbContext context, ILogger<IssueController> logger)
        {
            _context = context;
            _logger = logger;
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
        public async Task<IActionResult> ReportIssue(Issue issue, IFormFile mediaAttachment)
        {
            if (HttpContext.Session.GetString("LoggedInUser") == null)
            {
                return RedirectToAction("Login", "Account");
            }

            // Remove User and UserId from ModelState as they will be set manually
            ModelState.Remove("User");
            ModelState.Remove("UserId");

            if (ModelState.IsValid)
            {
                try
                {
                    issue.ReportDateTime = DateTime.Now;
                    var username = HttpContext.Session.GetString("LoggedInUser");
                    var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
                    if (user == null)
                    {
                        return RedirectToAction("Login", "Account");
                    }
                    issue.UserId = user.UserId;

                    if (mediaAttachment != null && mediaAttachment.Length > 0)
                    {
                        var fileName = Path.GetFileName(mediaAttachment.FileName);
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", fileName);
                        Directory.CreateDirectory(Path.GetDirectoryName(filePath)); // Ensure the directory exists
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await mediaAttachment.CopyToAsync(stream);
                        }
                        issue.MediaAttachment = "/uploads/" + fileName;
                    }
                    else
                    {
                        issue.MediaAttachment = "No attachment";
                    }

                    _context.Issues.Add(issue);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Issue reported successfully!";
                    return RedirectToAction("ReportSuccess");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while reporting an issue.");
                    ModelState.AddModelError("", "An error occurred while reporting the issue. Please try again.");
                }
            }
            return View(issue);
        }

        public IActionResult ReportSuccess()
        {
            return View();
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
                _logger.LogError(ex, "An error occurred while retrieving reports.");
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
                _logger.LogError(ex, "An error occurred while retrieving issue details.");
                TempData["ErrorMessage"] = "An error occurred while retrieving issue details. Please try again.";
                return RedirectToAction("ViewReports");
            }
        }
    }
}
