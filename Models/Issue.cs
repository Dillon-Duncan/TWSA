using System.ComponentModel.DataAnnotations;

namespace TWSA.Models
{
    public class Issue
    {
        [Key]
        public int IssueId { get; set; }

        public int UserId { get; set; }
        public string Subject { get; set; }
        public string Location { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string MediaAttachment { get; set; }
        public DateTime ReportDateTime { get; set; }
    }
}