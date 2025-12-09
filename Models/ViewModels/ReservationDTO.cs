using System;
using AirBB.Models.DomainModels;
using AirBB.Models.DataLayer;
using Microsoft.EntityFrameworkCore;

namespace AirBB.Models.ViewModels
{
    public class ReservationDTO
    {
        public int ReservationId { get; set; }
        public int ResidenceId { get; set; }
        public DateTime ReservationStartDate { get; set; }
        public DateTime ReservationEndDate { get; set; }

        public ReservationDTO() { }

        public ReservationDTO(Reservation r)
        {
            ReservationId = r.ReservationId;
            ResidenceId = r.ResidenceId;
            ReservationStartDate = r.ReservationStartDate;
            ReservationEndDate = r.ReservationEndDate;
        }

        public Reservation ToReservation(AirBBContext context)
        {
            var residence = context.Residences
                .Include(r => r.Location)
                .FirstOrDefault(r => r.ResidenceId == ResidenceId) ?? new Residence();

            return new Reservation
            {
                ReservationId = ReservationId,
                ResidenceId = ResidenceId,
                Residence = residence,
                ReservationStartDate = ReservationStartDate,
                ReservationEndDate = ReservationEndDate
            };
        }
    }
}
