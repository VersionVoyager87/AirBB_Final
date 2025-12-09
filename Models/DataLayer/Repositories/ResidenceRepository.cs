using Microsoft.EntityFrameworkCore;
using AirBB.Models.DataLayer.Configuration;
using AirBB.Models.DomainModels;
using AirBB.Models.ViewModels;

namespace AirBB.Models.DataLayer.Repositories
{
    /// <summary>
    /// Repository for Residence entities with specialized methods.
    /// </summary>
    public class ResidenceRepository : Repository<Residence>
    {
        public ResidenceRepository(AirBBContext context) : base(context) { }

        /// <summary>
        /// Get all residences with their locations (for list views)
        /// </summary>
        public List<Residence> GetAllWithLocations()
        {
            return _context.Residences!
                .Include(r => r.Location)
                .OrderBy(r => r.Name)
                .ToList();
        }

        /// <summary>
        /// Get a residence by ID with all related data
        /// </summary>
        public Residence? GetByIdWithDetails(int residenceId)
        {
            return _context.Residences!
                .Include(r => r.Location)
                .FirstOrDefault(r => r.ResidenceId == residenceId);
        }

        /// <summary>
        /// Get filtered residences based on criteria
        /// </summary>
        public List<Residence> GetFilteredResidences(FilterCriteria criteria)
        {
            var query = _context.Residences!
                .Include(r => r.Location)
                .AsQueryable();

            // Where filter
            if (!string.IsNullOrEmpty(criteria.ActiveLocation) && criteria.ActiveLocation.ToLower() != "all")
            {
                query = query.Where(r =>
                    r.Location != null &&
                    r.Location.Name.ToLower() == criteria.ActiveLocation.ToLower());
            }

            // When filter
            if (!string.IsNullOrEmpty(criteria.StartDate) && !string.IsNullOrEmpty(criteria.EndDate))
            {
                if (DateTime.TryParse(criteria.StartDate, out DateTime start) &&
                    DateTime.TryParse(criteria.EndDate, out DateTime end))
                {
                    var reservedIds = _context.Reservations!
                        .Where(r =>
                            r.ReservationStartDate <= end &&
                            r.ReservationEndDate >= start)
                        .Select(r => r.ResidenceId)
                        .Distinct()
                        .ToList();

                    query = query.Where(r => !reservedIds.Contains(r.ResidenceId));
                }
            }

            // Who filter
            if (criteria.GuestCount > 0)
            {
                query = query.Where(r => r.GuestNumber >= criteria.GuestCount);
            }

            return query.OrderBy(r => r.Name).ToList();
        }

        /// <summary>
        /// Get residences by location ID
        /// </summary>
        public List<Residence> GetByLocationId(int locationId)
        {
            return _context.Residences!
                .Where(r => r.LocationId == locationId)
                .Include(r => r.Location)
                .OrderBy(r => r.Name)
                .ToList();
        }

        /// <summary>
        /// Get residences by owner ID
        /// </summary>
        public List<Residence> GetByOwnerId(int ownerId)
        {
            return _context.Residences!
                .Where(r => r.OwnerId == ownerId)
                .Include(r => r.Location)
                .OrderBy(r => r.Name)
                .ToList();
        }
    }
}
