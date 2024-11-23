using System.ComponentModel.DataAnnotations.Schema;

namespace POS_System.Business.Dtos.TimeSlotDtos
{
    public record CreateTimeSlotDto
    {
        //Foreign keys
        [ForeignKey("Employee")]
        public int EmployeeVersionId { get; set; }

        //Fields
        public required DateTime StartTime { get; set; }
        public required bool IsAvailable { get; set; }
    }
}
