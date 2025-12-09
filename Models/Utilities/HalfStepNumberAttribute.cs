using System.ComponentModel.DataAnnotations;

namespace AirBB.Models.Utilities
{
    public class HalfStepNumberAttribute : ValidationAttribute
    {
        public HalfStepNumberAttribute()
        {
            ErrorMessage = "Bathrooms must be in 0.5 steps (e.g., 1.0, 1.5, 2.0).";
        }

        public override bool IsValid(object? value)
        {
            if (value == null) return true;

            decimal number = Convert.ToDecimal(value);
            return number * 2 == Math.Floor(number * 2);
        }
    }
}
