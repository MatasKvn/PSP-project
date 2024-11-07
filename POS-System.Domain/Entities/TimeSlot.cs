using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_System.Domain.Entities
{
    [Table("TimeSlots")]
    public record TimeSlot
    {
        [Key]
        public int Id { get; init; }
        [ForeignKey("Employee")]
        public int EmployeeId { get; init; }
        [ForeignKey("Employee")]
        public DateTime EmployeeVersion { get; init; }
        public DateTime StartTime { get; init; }
        public bool IsAvailable { get; init; }
    }
}
