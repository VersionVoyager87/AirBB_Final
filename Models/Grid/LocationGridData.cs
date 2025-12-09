using AirBB.Models.DomainModels;

namespace AirBB.Models.Grid
{
    
    public class LocationGridData
    {
        public int LocationId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public int ResidenceCount { get; set; }

        public LocationGridData(Location location, int residenceCount = 0)
        {
            LocationId = location.LocationId;
            Name = location.Name;
            Region = location.Region;
            ResidenceCount = residenceCount;
        }

        public LocationGridData() { }
    }
}
