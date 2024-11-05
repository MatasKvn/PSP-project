using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_System.Domain.Entities
{
    [Table("TimeSlots")]
    public record TimeSlot(int id, int employeeId, DateTime startDate, bool isAvailable = true)
    {
        [Key]
        public int Id { get; init; } = id;
        public int EmployeeId { get; init; } = employeeId;
        public DateTime StartTime { get; init; } = startDate;
        public bool IsAvailable { get; init; } = isAvailable;
    }
}
