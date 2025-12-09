using System.ComponentModel.DataAnnotations;

namespace AirBB.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string? Name { get; set; }

        [Phone]
        public string? PhoneNumber { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? SSN { get; set; }

        [Required]
        public string? UserType { get; set; } // Owner, Admin, Client
    }
}
