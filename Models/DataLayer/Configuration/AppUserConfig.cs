using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AirBB.Models.DomainModels;

namespace AirBB.Models.DataLayer.Configuration
{
    public class AppUserConfig : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> entity)
        {
            entity.HasKey(u => u.AppUserId);

            entity.Property(u => u.Name)
                  .IsRequired()
                  .HasMaxLength(50);

            entity.Property(u => u.Email)
                  .HasMaxLength(100);

            entity.Property(u => u.PhoneNumber)
                  .HasMaxLength(20);

            entity.Property(u => u.SSN)
                  .IsRequired()
                  .HasMaxLength(11);

            entity.Property(u => u.Password)
                  .IsRequired()
                  .HasMaxLength(25);

            entity.Property(u => u.UserType)
                  .IsRequired()
                  .HasMaxLength(20);

            entity.Ignore(u => u.ConfirmPassword);     // Not mapped
            
            // Seed 2 admin-level users
            entity.HasData(
                new AppUser
                {
                    AppUserId = 1,
                    Name = "Admin User",
                    Email = "admin@airbb.com",
                    PhoneNumber = "1234567890",
                    DOB = new DateTime(1990, 1, 1),
                    SSN = "111-11-1111",
                    Password = "adminpass",
                    ConfirmPassword = "adminpass",  // Will be ignored by EF
                    UserType = "Admin"
                },
                new AppUser
                {
                    AppUserId = 2,
                    Name = "Client User",
                    Email = "client@airbb.com",
                    PhoneNumber = "9876543210",
                    DOB = new DateTime(1995, 5, 5),
                    SSN = "222-22-2222",
                    Password = "clientpass",
                    ConfirmPassword = "clientpass", // Will be ignored by EF
                    UserType = "Client"
                }
            );
        }
    }
}

