using AirBB.Models.DomainModels;

namespace AirBB.Models.Grid
{
    
    public class ReservationGridData
    {
        public int ReservationId { get; set; }
        public int ResidenceId { get; set; }
        public string ResidenceName { get; set; } = string.Empty;
        public string LocationName { get; set; } = string.Empty;
        public DateTime ReservationStartDate { get; set; }
        public DateTime ReservationEndDate { get; set; }
        public int DaysReserved { get; set; }
        public decimal TotalPrice { get; set; }

        public ReservationGridData(Reservation reservation)
        {
            ReservationId = reservation.ReservationId;
            ResidenceId = reservation.ResidenceId;
            ResidenceName = reservation.Residence?.Name ?? "Unknown";
            LocationName = reservation.Residence?.Location?.Name ?? "Unknown";
            ReservationStartDate = reservation.ReservationStartDate;
            ReservationEndDate = reservation.ReservationEndDate;
            DaysReserved = (int)(reservation.ReservationEndDate - reservation.ReservationStartDate).TotalDays;
            TotalPrice = DaysReserved * (reservation.Residence?.PricePerNight ?? 0);
        }

        public ReservationGridData() { }
    }
}
