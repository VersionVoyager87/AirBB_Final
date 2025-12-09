using System.ComponentModel.DataAnnotations;

namespace AirBB.Models.Utilities
{
    public class PastYearAttribute : ValidationAttribute
    {
        private int _maxYearsAgo;

        public PastYearAttribute(int maxYearsAgo)
        {
            _maxYearsAgo = maxYearsAgo;
            ErrorMessage = $"Built year must be within the last {_maxYearsAgo} years.";
        }

        public override bool IsValid(object? value)
        {
            if (value == null) return true;

            int year = Convert.ToInt32(value);
            int currentYear = DateTime.Now.Year;

            return year >= currentYear - _maxYearsAgo && year <= currentYear;
        }
    }
}
