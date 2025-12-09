using Microsoft.EntityFrameworkCore;
using AirBB.Models.DataLayer.Configuration;
using AirBB.Models.DomainModels;

namespace AirBB.Models.DataLayer.Repositories
{
    /// <summary>
    /// Repository for AppUser entities.
    /// </summary>
    public class AppUserRepository : Repository<AppUser>
    {
        public AppUserRepository(AirBBContext context) : base(context) { }

        /// <summary>
        /// Get user by email
        /// </summary>
        public AppUser? GetByEmail(string email)
        {
            return _context.AppUsers!.FirstOrDefault(u => u.Email!.ToLower() == email.ToLower());
        }

        /// <summary>
        /// Get all users of a specific type (Owner, Admin, Client)
        /// </summary>
        public List<AppUser> GetByUserType(string userType)
        {
            return _context.AppUsers!
                .Where(u => u.UserType == userType)
                .OrderBy(u => u.Name)
                .ToList();
        }

        /// <summary>
        /// Check if email exists
        /// </summary>
        public bool EmailExists(string email, int? excludeUserId = null)
        {
            var query = _context.AppUsers!.Where(u => u.Email!.ToLower() == email.ToLower());
            
            if (excludeUserId.HasValue)
            {
                query = query.Where(u => u.AppUserId != excludeUserId.Value);
            }

            return query.Any();
        }

        /// <summary>
        /// Get all owners
        /// </summary>
        public List<AppUser> GetAllOwners()
        {
            return GetByUserType("Owner");
        }

        /// <summary>
        /// Get all admins
        /// </summary>
        public List<AppUser> GetAllAdmins()
        {
            return GetByUserType("Admin");
        }
    }
}
