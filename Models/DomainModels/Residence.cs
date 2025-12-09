using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using AirBB.Models.Utilities;

namespace AirBB.Models.DomainModels
{
    public class Residence
    {
        public int ResidenceId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = "";

        [Required(ErrorMessage = "Please select a location.")]
        public int LocationId { get; set; }
        public Location? Location { get; set; }

        [Required(ErrorMessage = "Please enter Owner ID.")]
        [Remote("CheckOwner", "Validation", areaName: "Admin",
            ErrorMessage = "Owner ID is invalid or not registered as Owner.")]
        public int OwnerId { get; set; }

        [Required]
        [StringLength(100)]
        public string Accommodation { get; set; } = "";

        [Range(0, 20)]
        public int Bedrooms { get; set; }

        [HalfStepNumber]
        public decimal Bathrooms { get; set; }

        [PastYear(150)]
        public int BuiltYear { get; set; }

        
        public int GuestNumber { get; set; }

        [Display(Name = "Price Per Night")]
        public decimal PricePerNight { get; set; }

        public string? ResidencePicture { get; set; }

        
        public int BedroomNumber => Bedrooms;
        public decimal BathroomNumber => Bathrooms;
        public decimal Price => PricePerNight;
        public string? Image => ResidencePicture;
    }
}
