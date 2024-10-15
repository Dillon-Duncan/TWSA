using System.Collections.Generic;
using TWSA.Models;

namespace TWSA.Data
{
    public static class EventAnnouncementData
    {
        private static readonly List<EventAnnouncement> events = new List<EventAnnouncement>();

        public static void AddEvent(EventAnnouncement eventAnnouncement)
        {
            events.Add(eventAnnouncement);
        }

        public static List<EventAnnouncement> GetAllEvents()
        {
            return new List<EventAnnouncement>(events);
        }

        public static EventAnnouncement GetEvent(int eventId)
        {
            return events.Find(e => e.EventId == eventId);
        }

        public static void UpdateEvent(EventAnnouncement eventAnnouncement)
        {
            var existingEvent = GetEvent(eventAnnouncement.EventId);
            if (existingEvent != null)
            {
                events.Remove(existingEvent);
                events.Add(eventAnnouncement);
            }
        }

        public static void DeleteEvent(int eventId)
        {
            var eventAnnouncement = GetEvent(eventId);
            if (eventAnnouncement != null)
            {
                events.Remove(eventAnnouncement);
            }
        }
    }
}