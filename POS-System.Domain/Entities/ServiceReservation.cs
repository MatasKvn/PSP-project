using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_System.Domain.Entities
{
    [Table("ServiceReservations")]
    public record ServiceReservation
    {
        //Primary key
        [Key]
        public int Id { get; init; }

        //Foreign keys
        [ForeignKey("CartItem")]
        public int CartItemId { get; init; }
        [ForeignKey("Timeslot")]
        public int TimeSlotId { get; init; }

        //Navigation properties
        public virtual CartItem CartItem { get; set; }
        public virtual TimeSlot TimeSlot { get; set; }

        //Fields
        public required DateTime BookingTime { get; init; }
        [MaxLength(40)]
        public required string CustomerName { get; init; }
        [MaxLength(15)]
        public required string CustomerPhone { get; init; }
    }
}
