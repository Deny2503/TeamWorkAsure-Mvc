using System.ComponentModel.DataAnnotations;

namespace WebApplication16.Data.Models
{
    public class Admin
    {
        [Key]
        public int UserId { get; set; }

        public User User { get; set; } = null!;
    }
}
