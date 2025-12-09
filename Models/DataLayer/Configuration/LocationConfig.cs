using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AirBB.Models.DomainModels;

namespace AirBB.Models.DataLayer.Configuration
{
    public class LocationConfig : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> entity)
        {
            entity.HasKey(l => l.LocationId);

            entity.Property(l => l.Name)
                  .IsRequired()
                  .HasMaxLength(50);

            entity.Property(l => l.Region)
                  .IsRequired()
                  .HasMaxLength(50);

            // Seed locations
            entity.HasData(
                new Location { LocationId = 1, Name = "Chicago", Region = "Illinois" },
                new Location { LocationId = 2, Name = "New York", Region = "New York" },
                new Location { LocationId = 3, Name = "Miami", Region = "Florida" },
                new Location { LocationId = 4, Name = "Atlanta", Region = "Georgia" }
            );
        }
    }
}
