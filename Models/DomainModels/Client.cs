using System.ComponentModel.DataAnnotations;
using AirBB.Models.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace AirBB.Models.DomainModels
{
    [RequiredContactInfo] // Phone OR Email must be provided
    public class Client
    {
        public int ClientId { get; set; }

        [Required(ErrorMessage = "Full name is required.")]
        [StringLength(50)]
        public string FullName { get; set; } = string.Empty;

        [Phone(ErrorMessage = "Invalid phone number.")]
        public string? PhoneNumber { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [Remote("CheckEmail", "Validation")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Please select user type.")]
        public string UserType { get; set; } = string.Empty;  // Owner/Admin/Client
    }
}
