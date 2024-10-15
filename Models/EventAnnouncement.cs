using System;
using System.ComponentModel.DataAnnotations;

namespace TWSA.Models
{
    public class EventAnnouncement
    {
        [Key]
        public int EventId { get; set; }

        [Required]
        public bool IsEvent { get; set; }

        [Required(ErrorMessage = "Subject is required")]
        [StringLength(100, ErrorMessage = "Subject cannot be longer than 100 characters")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Category is required")]
        [StringLength(50, ErrorMessage = "Category cannot be longer than 50 characters")]
        public string Category { get; set; }

        [Required(ErrorMessage = "Location is required")]
        [StringLength(100, ErrorMessage = "Location cannot be longer than 100 characters")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Event date and time is required")]
        public DateTime EventDateTime { get; set; }

        [StringLength(255)]
        public string MediaAttachment { get; set; }
    }
}
