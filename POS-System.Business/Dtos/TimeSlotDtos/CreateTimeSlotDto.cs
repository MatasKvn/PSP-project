namespace POS_System.Business.Dtos.TimeSlotDtos
{
    public record CreateTimeSlotDto
    {
        public int EmployeeVersionId { get; set; }
        public required DateTime StartTime { get; set; }
        public required bool IsAvailable { get; set; }
    }
}
