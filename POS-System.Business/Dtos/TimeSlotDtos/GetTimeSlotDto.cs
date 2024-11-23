using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_System.Business.Dtos.TimeSlotDtos
{
    public record GetTimeSlotDto
    {
        [Key]
        public int Id { get; set; }

        //Foreign keys
        [ForeignKey("Employee")]
        public int EmployeeVersionId { get; set; }

        //Fields
        public required DateTime StartTime { get; set; }
        public required bool IsAvailable { get; set; }
    }
}
