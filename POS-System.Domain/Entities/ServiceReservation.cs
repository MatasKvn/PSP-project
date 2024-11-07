using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_System.Domain.Entities
{
    [Table("ServiceReservations")]
    public record ServiceReservation
    {
        [Key]
        public int Id { get; init; }
        [ForeignKey("CartItem")]
        public int CartItemId { get; init; }
        [ForeignKey("CartItem")]
        public DateTime CartItemVersion { get; init; }
        public int TimeSlotId { get; init; }
        public DateTime BookingTime { get; init; }
        [MaxLength(40)]
        public required string CustomerName { get; init; }
        [MaxLength(15)]
        public required string CustomerPhone { get; init; }
    }
}
