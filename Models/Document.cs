using System.ComponentModel.DataAnnotations;

namespace WebApplication16.Data.Models
{
    public class Document
    {
        public enum DocumentStatus
        {
            Draft,
            InReview,
            Approved,
            Rejected
        }

        [Key]
        public int Id { get; set; }

        [MaxLength(200)]
        public string Title { get; set; } = null!;

        public string BlobName { get; set; } = null!;

        public DocumentStatus Status { get; set; }

        public int UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
