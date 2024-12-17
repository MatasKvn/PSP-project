namespace POS_System.Business.Dtos.Request
{
    public record ServiceReservationRequest
    {
        public int CartItemId { get; set; }
        public int TimeSlotId { get; set; }
        public required DateTime BookingTime { get; set; }
        public required string CustomerName { get; set; }
        public required string CustomerPhone { get; set; }
        public required bool IsCancelled { get; set; }
    }
}
