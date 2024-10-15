using TWSA.Models;

namespace TWSA.Data
{
    public class EventAnnouncementRepository
    {
        private readonly ApplicationDbContext _context;

        public EventAnnouncementRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddEvent(EventAnnouncement eventAnnouncement)
        {
            _context.EventAnnouncements.Add(eventAnnouncement);
            _context.SaveChanges();
        }

        public EventAnnouncement GetEvent(int eventId)
        {
            return _context.EventAnnouncements.FirstOrDefault(e => e.EventId == eventId);
        }

        public List<EventAnnouncement> GetAllEvents()
        {
            return _context.EventAnnouncements.ToList();
        }

        public void UpdateEvent(EventAnnouncement eventAnnouncement)
        {
            _context.EventAnnouncements.Update(eventAnnouncement);
            _context.SaveChanges();
        }

        public void DeleteEvent(int eventId)
        {
            var eventAnnouncement = GetEvent(eventId);
            if (eventAnnouncement != null)
            {
                _context.EventAnnouncements.Remove(eventAnnouncement);
                _context.SaveChanges();
            }
        }
    }
}