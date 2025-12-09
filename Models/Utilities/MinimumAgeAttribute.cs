using System.ComponentModel.DataAnnotations;

namespace AirBB.Models.Utilities
{
    public class MinimumAgeAttribute : ValidationAttribute
    {
        private readonly int _minAge;

        public MinimumAgeAttribute(int minAge)
        {
            _minAge = minAge;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext context)
        {
            if (value is DateTime dob)
            {
                var age = DateTime.Today.Year - dob.Year;
                if (dob > DateTime.Today.AddYears(-age)) age--;

                return (age < _minAge)
                    ? new ValidationResult(ErrorMessage ?? $"Must be at least {_minAge} years old.")
                    : ValidationResult.Success;
            }

            return new ValidationResult("Invalid date format.");
        }
    }
}
