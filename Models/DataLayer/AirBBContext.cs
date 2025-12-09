using Microsoft.EntityFrameworkCore;
using AirBB.Models.DomainModels;
using AirBB.Models.DataLayer.Configuration;

namespace AirBB.Models.DataLayer
{
    public class AirBBContext : DbContext
    {
        public AirBBContext(DbContextOptions<AirBBContext> options)
            : base(options)
        {
        }

        // -------------------- DbSets --------------------
        public DbSet<Location> Locations { get; set; } = null!;
        public DbSet<Residence> Residences { get; set; } = null!;
        public DbSet<Reservation> Reservations { get; set; } = null!;
        public DbSet<AppUser> AppUsers { get; set; } = null!;
        public DbSet<Client> Clients { get; set; } = null!;


        // -------------------- Model Configuration --------------------
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply all entity configurations from the Configuration folder
            modelBuilder.ApplyConfiguration(new LocationConfig());
            modelBuilder.ApplyConfiguration(new ResidenceConfig());
            modelBuilder.ApplyConfiguration(new ReservationConfig());
            modelBuilder.ApplyConfiguration(new AppUserConfig());
            modelBuilder.ApplyConfiguration(new ClientConfig());
        }
    }
}
