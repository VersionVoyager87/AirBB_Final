using System.ComponentModel.DataAnnotations;
using AirBB.Models.DomainModels;

namespace AirBB.Models.Utilities
{
    public class RequiredContactInfoAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            // Get the object being validated (AppUser)
            var user = validationContext.ObjectInstance as AppUser;

            // If null, fail immediately
            if (user == null)
            {
                return new ValidationResult("Unexpected error: user instance is null.");
            }

            // Check if both phone and email are empty/null
            bool hasEmail = !string.IsNullOrWhiteSpace(user.Email);
            bool hasPhone = !string.IsNullOrWhiteSpace(user.PhoneNumber);

            if (!hasEmail && !hasPhone)
            {
                return new ValidationResult("Please provide either a phone number or email address.");
            }

            return ValidationResult.Success;
        }
    }
}
