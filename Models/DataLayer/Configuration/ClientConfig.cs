using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AirBB.Models.DomainModels;

namespace AirBB.Models.DataLayer.Configuration
{
    public class ClientConfig : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> entity)
        {
            entity.HasKey(c => c.ClientId);

            entity.Property(c => c.FullName)
                  .IsRequired()
                  .HasMaxLength(50);

            entity.Property(c => c.Email)
                  .HasMaxLength(100);

            entity.Property(c => c.PhoneNumber)
                  .HasMaxLength(20);

            entity.Property(c => c.UserType)
                  .IsRequired()
                  .HasMaxLength(20);
        }
    }
}
