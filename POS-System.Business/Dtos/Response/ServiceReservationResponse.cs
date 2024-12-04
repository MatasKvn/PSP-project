namespace POS_System.Business.Dtos.Response
{
    public record ServiceReservationResponse
    {
        public int Id { get; set; }
        public int CartItemId { get; set; }
        public int TimeSlotId { get; set; }
        public required DateTime BookingTime { get; set; }
        public required string CustomerName { get; set; }
        public required string CustomerPhone { get; set; }
    }
}
