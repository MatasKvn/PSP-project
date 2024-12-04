namespace POS_System.Business.Dtos.Response
{
    public record TimeSlotResponse
    {
        public int Id { get; set; }
        public int EmployeeVersionId { get; set; }
        public required DateTime StartTime { get; set; }
        public required bool IsAvailable { get; set; }
    }
}
