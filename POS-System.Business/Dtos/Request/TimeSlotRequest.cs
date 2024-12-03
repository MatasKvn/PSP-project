namespace POS_System.Business.Dtos.Request
{
    public record TimeSlotRequest
    {
        public int EmployeeVersionId { get; set; }
        public required DateTime StartTime { get; set; }
        public required bool IsAvailable { get; set; }
    }
}
