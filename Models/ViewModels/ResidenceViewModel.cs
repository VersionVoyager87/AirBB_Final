using AirBB.Models.DomainModels;

namespace AirBB.Models.ViewModels
{
    // Combines everything the view needs: filters, lists, and feedback
    public class ResidenceViewModel
    {
        // Filtering options and current selection
        public FilterCriteria Criteria { get; set; } = new();

        // Lists used to populate dropdowns or results
        public List<Location> Locations { get; set; } = new();
        public List<Residence> Residences { get; set; } = new();
        public List<Reservation> Reservations { get; set; } = new();

        public Residence? Residence { get; set; }

        // Feedback message from TempData (PRG pattern)
        public string Message { get; set; } = string.Empty;

        // --- Helper methods for UI highlighting ---
        public string CheckActiveLocation(string loc) =>
            loc.ToLower() == Criteria.ActiveLocation.ToLower() ? "active" : "";

        public string CheckGuestCount(int guests) =>
            guests == Criteria.GuestCount ? "active" : "";
    }
}
