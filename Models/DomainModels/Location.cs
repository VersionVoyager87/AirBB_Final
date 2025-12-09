using System.ComponentModel.DataAnnotations;

namespace AirBB.Models.DomainModels
{
    public class Location
    {
        public int LocationId { get; set; }

        [Required(ErrorMessage = "Please enter a location name.")]
        [StringLength(50, ErrorMessage = "Location name cannot exceed 50 characters.")]
        [RegularExpression("^[a-zA-Z ]+$", ErrorMessage = "Only alphabetic characters allowed.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a region name.")]
        [StringLength(50)]
        public string Region { get; set; } = string.Empty;
    }
}
