using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_System.Domain.Entities
{
    [Table("ServiceReservations")]
    public class ServiceReservation(int id, int cartItemId, int timeSlotId, DateTime bookingTime, string customerName, string customerPhone)
    {
        [Key]
        public int Id { get; } = id;
        [Required]
        public int CartItemId { get;} = cartItemId;
        [Required]
        public int TimeSlotId { get;} = timeSlotId;
        [Required]
        public DateTime BookingTime { get; } = bookingTime;
        [Required]
        [MaxLength(40)]
        public string CustomerName { get; } = customerName;
        [Required]
        [MaxLength(15)]
        public string CustomerPhone { get; } = customerPhone;
    }
}
