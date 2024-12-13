using POS_System.Common.Enums;

namespace POS_System.Business.Dtos.Response
{
    public record CartResponse
    {
        public int Id { get; set; }

        public int EmployeeVersionId { get; set; }

        public DateTime DateCreated { get; set; }

        public required string? DiscountId { get; set; }

        public required CartStatusEnum Status { get; set; }
    }
}
