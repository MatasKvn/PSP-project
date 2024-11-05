using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_System.Domain.Entities
{
    [Table("ServiceReservations")]
    public record ServiceReservation(int id, int cartItemId, int timeSlotId, DateTime bookingTime, string customerName, string customerPhone)
    {
        [Key]
        public int Id { get; init; } = id;
        public int CartItemId { get; init; } = cartItemId;
        public int TimeSlotId { get; init; } = timeSlotId;
        public DateTime BookingTime { get; init; } = bookingTime;
        [MaxLength(40)]
        public string CustomerName { get; init; } = customerName;
        [MaxLength(15)]
        public string CustomerPhone { get; init; } = customerPhone;
    }
}
