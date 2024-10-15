using System.ComponentModel.DataAnnotations;

namespace TWSA.Models
{
    public class EventAnnouncement
    {
        [Key]
        public int EventId { get; set; }

        public bool IsEvent { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Location { get; set; }
        public DateTime EventDateTime { get; set; }
        public string MediaAttachment { get; set; }
    }
}