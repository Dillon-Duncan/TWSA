using Microsoft.AspNetCore.Mvc;
using TWSA.Data;
using TWSA.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

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
            var events = await _context.EventAnnouncements.ToListAsync();
            return View(events);
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
                _context.EventAnnouncements.Add(eventAnnouncement);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Event posted successfully!";
                return RedirectToAction("LocalEvents");
            }
            return View(eventAnnouncement);
        }
    }
}
