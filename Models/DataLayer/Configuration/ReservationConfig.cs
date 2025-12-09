using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AirBB.Models.DomainModels;

namespace AirBB.Models.DataLayer.Configuration
{
    public class ReservationConfig : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> entity)
        {
            entity.HasKey(r => r.ReservationId);

            entity.Property(r => r.ReservationStartDate)
                  .IsRequired()
                  .HasColumnType("datetime");

            entity.Property(r => r.ReservationEndDate)
                  .IsRequired()
                  .HasColumnType("datetime");

            // Foreign key relationship to Residence
            entity.HasOne(r => r.Residence)
                  .WithMany()
                  .HasForeignKey(r => r.ResidenceId)
                  .OnDelete(DeleteBehavior.Cascade)
                  .IsRequired();
        }
    }
}
