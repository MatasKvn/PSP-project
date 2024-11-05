using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_System.Domain.Entities
{
    [Table("EmployeeServices")]
    public record EmployeeService(int employeeId, DateTime employeeVersion, int serviceId, DateTime serviceVersion)
    {
        [Key, Column(Order = 0)]
        public int EmployeeId { get; init; } = employeeId;
        [Key, Column(Order = 1)]
        public DateTime EmployeeVersion { get; init; } = employeeVersion;

        [Key, Column(Order = 2)]
        public int ServiceId { get; init; } = serviceId;
        [Key, Column(Order = 3)]
        public DateTime ServiceVersion { get; init; } = serviceVersion;
    }
}
