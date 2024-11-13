using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_System.Domain.Entities
{
    [Table("TimeSlots")]
    public record TimeSlot
    {
        //Primary key
        [Key]
        public int Id { get; init; }

        //Foreign keys
        [ForeignKey("Employee")]
        public int EmployeeVersionId { get; init; }

        //Navigation properties
        public virtual ServiceReservation ServiceReservation { get; set; }

        //Fields
        public required DateTime StartTime { get; init; }
        public required bool IsAvailable { get; init; }
    }
}
