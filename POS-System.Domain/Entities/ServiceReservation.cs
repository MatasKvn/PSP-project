using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_System.Domain.Entities
{
    [Table("ServiceReservations")]
    public record ServiceReservation
    {
        //Primary key
        [Key]
        public int Id { get; set; }

        //Foreign keys
        [ForeignKey("CartItem")]
        public int CartItemId { get; set; }
        [ForeignKey("Timeslot")]
        public int? TimeSlotId { get; set; }

        //Navigation properties
        public virtual CartItem CartItem { get; set; }
        public virtual TimeSlot? TimeSlot { get; set; }

        //Fields
        public required DateTime BookingTime { get; set; }
        [MaxLength(40)]
        public required string CustomerName { get; set; }
        [MaxLength(15)]
        public required string CustomerPhone { get; set; }
        public required bool isCancelled { get; set; }
    }
}
