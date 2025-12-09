using AirBB.Models.DomainModels;

namespace AirBB.Models.ViewModels
{
    public class ResidenceAdminViewModel
    {
        public Residence Residence { get; set; } = new Residence();
        public List<Location> Locations { get; set; } = new List<Location>();
        public List<AppUser> Owners { get; set; } = new List<AppUser>();
    }
}
