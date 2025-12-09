using Microsoft.EntityFrameworkCore;
using AirBB.Models.DataLayer.Configuration;
using AirBB.Models.DomainModels;

namespace AirBB.Models.DataLayer.Repositories
{
    /// <summary>
    /// Repository for Reservation entities.
    /// </summary>
    public class ReservationRepository : Repository<Reservation>
    {
        public ReservationRepository(AirBBContext context) : base(context) { }

        /// <summary>
        /// Get all reservations with residence details
        /// </summary>
        public List<Reservation> GetAllWithDetails()
        {
            return _context.Reservations!
                .Include(r => r.Residence)
                    .ThenInclude(res => res!.Location)
                .OrderByDescending(r => r.ReservationStartDate)
                .ToList();
        }

        /// <summary>
        /// Get reservations for a specific residence
        /// </summary>
        public List<Reservation> GetByResidenceId(int residenceId)
        {
            return _context.Reservations!
                .Where(r => r.ResidenceId == residenceId)
                .Include(r => r.Residence)
                    .ThenInclude(res => res!.Location)
                .OrderBy(r => r.ReservationStartDate)
                .ToList();
        }

        /// <summary>
        /// Get reservations that overlap with the given date range
        /// </summary>
        public List<Reservation> GetOverlappingReservations(DateTime startDate, DateTime endDate, int? excludeResidenceId = null)
        {
            var query = _context.Reservations!
                .Where(r =>
                    r.ReservationStartDate <= endDate &&
                    r.ReservationEndDate >= startDate);

            if (excludeResidenceId.HasValue)
            {
                query = query.Where(r => r.ResidenceId != excludeResidenceId.Value);
            }

            return query
                .Include(r => r.Residence)
                    .ThenInclude(res => res!.Location)
                .ToList();
        }

        /// <summary>
        /// Check if a residence is available for the given date range
        /// </summary>
        public bool IsAvailable(int residenceId, DateTime startDate, DateTime endDate)
        {
            return !_context.Reservations!.Any(r =>
                r.ResidenceId == residenceId &&
                r.ReservationStartDate <= endDate &&
                r.ReservationEndDate >= startDate);
        }
    }
}
