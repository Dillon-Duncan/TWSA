using Microsoft.AspNetCore.Mvc;
using TWSA.Data;
using TWSA.Models;

namespace TWSA.Controllers
{
    public class EventController : Controller
    {
        // Displays a list of local events
        [HttpGet]
        public IActionResult LocalEvents()
        {
            var events = EventAnnouncementData.GetAllEvents();
            return View(events);
        }

        // Displays the form to post a new event
        [HttpGet]
        public IActionResult PostEvent()
        {
            return View();
        }

        // Handles the event posting form submission
        [HttpPost]
        public IActionResult PostEvent(EventAnnouncement eventAnnouncement)
        {
            if (ModelState.IsValid)
            {
                EventAnnouncementData.AddEvent(eventAnnouncement);
                return RedirectToAction("LocalEvents");
            }
            return View(eventAnnouncement);
        }
    }
}