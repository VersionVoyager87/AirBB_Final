using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;
using AirBB.Models.Utilities;

namespace AirBB.Models.DomainModels
{
    [RequiredContactInfo(ErrorMessage = "Please provide either a phone number or an email address.")]
    public class AppUser
    {
        public int AppUserId { get; set; }

        [Required(ErrorMessage = "Please enter a username.")]
        [RegularExpression("^[a-zA-Z0-9 ]+$", ErrorMessage = "Username may not contain special characters.")]
        public string Name { get; set; } = string.Empty;

        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        [Remote("CheckEmail", "Validation", ErrorMessage = "Email already in use.")]
        [Display(Name = "Email Address")]
        public string? Email { get; set; }

        [Phone(ErrorMessage = "Please enter a valid phone number.")]
        [Display(Name = "Phone Number")]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Please enter a date of birth.")]
        [MinimumAge(13, ErrorMessage = "You must be at least 13 years old.")]
        [Display(Name = "Date of Birth")]
        public DateTime? DOB { get; set; }

        [Required(ErrorMessage = "Please enter an SSN.")]
        [RegularExpression(@"^\d{3}-\d{2}-\d{4}$", ErrorMessage = "SSN must be in the format 123-45-6789.")]
        [Display(Name = "SSN")]
        public string SSN { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a password.")]
        [StringLength(25, ErrorMessage = "Please limit your password to 25 characters.")]
        [Compare("ConfirmPassword", ErrorMessage = "Passwords do not match.")]
        public string Password { get; set; } = string.Empty;

        [NotMapped]
        [Required(ErrorMessage = "Please confirm your password.")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please select a user type.")]
        [Display(Name = "User Type")]
        public string UserType { get; set; } = string.Empty;

        [NotMapped]
        public static Dictionary<string, string> UserTypeDict => new()
        {
            { "Owner", "Owner" },
            { "Admin", "Admin" },
            { "Client", "Client" }
        };
    }
}
