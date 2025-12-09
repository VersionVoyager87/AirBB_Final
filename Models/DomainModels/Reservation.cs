namespace AirBB.Models.DomainModels
{
    public class Reservation
    {
        public int ReservationId { get; set; }

        public DateTime ReservationStartDate { get; set; }
        public DateTime ReservationEndDate { get; set; }

        // FK + navigation
        public int ResidenceId { get; set; }
        public Residence? Residence { get; set; }
    }
}
