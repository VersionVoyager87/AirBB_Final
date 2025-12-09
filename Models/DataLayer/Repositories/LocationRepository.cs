using Microsoft.EntityFrameworkCore;
using AirBB.Models.DataLayer.Configuration;
using AirBB.Models.DomainModels;

namespace AirBB.Models.DataLayer.Repositories
{
    /// <summary>
    /// Repository for Location entities.
    /// </summary>
    public class LocationRepository : Repository<Location>
    {
        public LocationRepository(AirBBContext context) : base(context) { }

        /// <summary>
        /// Get all locations ordered by name
        /// </summary>
        public List<Location> GetAllOrdered()
        {
            return _context.Locations!.OrderBy(l => l.Name).ToList();
        }

        /// <summary>
        /// Get location by name
        /// </summary>
        public Location? GetByName(string name)
        {
            return _context.Locations!.FirstOrDefault(l => l.Name.ToLower() == name.ToLower());
        }
    }
}
