using Microsoft.EntityFrameworkCore;
using AirBB.Models.DataLayer.Configuration;
using AirBB.Models.DomainModels;

namespace AirBB.Models.DataLayer.Repositories
{
    /// <summary>
    /// Repository for Client entities.
    /// </summary>
    public class ClientRepository : Repository<Client>
    {
        public ClientRepository(AirBBContext context) : base(context) { }

        /// <summary>
        /// Get client by email
        /// </summary>
        public Client? GetByEmail(string email)
        {
            return _context.Clients!.FirstOrDefault(c => c.Email!.ToLower() == email.ToLower());
        }

        /// <summary>
        /// Get clients by user type
        /// </summary>
        public List<Client> GetByUserType(string userType)
        {
            return _context.Clients!
                .Where(c => c.UserType == userType)
                .OrderBy(c => c.FullName)
                .ToList();
        }

        /// <summary>
        /// Check if email exists
        /// </summary>
        public bool EmailExists(string email, int? excludeClientId = null)
        {
            var query = _context.Clients!.Where(c => c.Email!.ToLower() == email.ToLower());

            if (excludeClientId.HasValue)
            {
                query = query.Where(c => c.ClientId != excludeClientId.Value);
            }

            return query.Any();
        }

        /// <summary>
        /// Get all clients
        /// </summary>
        public List<Client> GetAllClients()
        {
            return GetByUserType("Client");
        }
    }
}
