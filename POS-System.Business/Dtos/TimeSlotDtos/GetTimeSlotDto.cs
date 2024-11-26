namespace POS_System.Business.Dtos.TimeSlotDtos
{
    public record GetTimeSlotDto
    {
        public int Id { get; set; }
        public int EmployeeVersionId { get; set; }
        public required DateTime StartTime { get; set; }
        public required bool IsAvailable { get; set; }
    }
}
