using Microsoft.AspNetCore.Mvc;
using TWSA.Data;
using TWSA.Models;

namespace TWSA.Controllers
{
    public class IssueController : Controller
    {
        // Displays the form to report an issue
        [HttpGet]
        public IActionResult ReportIssue()
        {
            return View();
        }

        // Handles the issue reporting form submission
        [HttpPost]
        public IActionResult ReportIssue(Issue issue)
        {
            if (ModelState.IsValid)
            {
                // Set the report date to the current time
                issue.ReportDateTime = DateTime.Now;

                // Logic to save the issue to a database can go here.
                IssueData.AddIssue(issue, "johndoe"); // Replace "johndoe" with the logged-in user's username

                return RedirectToAction("ViewReports");
            }
            return View(issue);
        }

        // Displays a list of reported issues
        [HttpGet]
        public IActionResult ViewReports()
        {
            var issues = IssueData.GetAllIssues();
            return View(issues);
        }
    }
}