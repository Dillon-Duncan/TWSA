using Microsoft.AspNetCore.Mvc;
using TWSA.Data;
using TWSA.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace TWSA.Controllers
{
    public class EventController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Displays a list of local events
        [HttpGet]
        public async Task<IActionResult> LocalEvents()
        {
            try
            {
                var events = await _context.EventAnnouncements.ToListAsync();
                return View(events);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while retrieving events. Please try again.";
                return RedirectToAction("Index", "Home");
            }
        }

        // Displays the form to post a new event
        [HttpGet]
        public IActionResult PostEvent()
        {
            if (HttpContext.Session.GetString("IsAdmin") != "True")
            {
                return RedirectToAction("AccessDenied", "Home");
            }
            return View();
        }

        // Handles the event posting form submission
        [HttpPost]
        public async Task<IActionResult> PostEvent(EventAnnouncement eventAnnouncement)
        {
            if (HttpContext.Session.GetString("IsAdmin") != "True")
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.EventAnnouncements.Add(eventAnnouncement);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Event posted successfully!";
                    return RedirectToAction("LocalEvents");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "An error occurred while posting the event. Please try again.");
                    return View(eventAnnouncement);
                }
            }
            return View(eventAnnouncement);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var eventAnnouncement = await _context.EventAnnouncements.FirstOrDefaultAsync(e => e.EventId == id);
                if (eventAnnouncement == null)
                {
                    return NotFound();
                }
                return View(eventAnnouncement);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while retrieving event details. Please try again.";
                return RedirectToAction("LocalEvents");
            }
        }
    }
}
