using System.ComponentModel.DataAnnotations;

namespace WebApplication16.Data.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(30)]
        public string Username { get; set; } = null!;

        [EmailAddress]
        public string Email { get; set; } = null!;

        public string HashPassword { get; set; } = null!;

        public IEnumerable<Document> Documents { get; set; } = null!;
    }
}
