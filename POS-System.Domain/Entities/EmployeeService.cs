using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_System.Domain.Entities
{
    [Table("EmployeeServices")]
    [PrimaryKey(nameof(EmployeeId), nameof(EmployeeVersion), nameof(ServiceId), nameof(ServiceVersion))]
    public record EmployeeService
    {
        [ForeignKey("Employee")]
        public int EmployeeId { get; init; }
        [ForeignKey("Employee")]
        public DateTime EmployeeVersion { get; init; }

        [ForeignKey("Service")]
        public int ServiceId { get; init; }
        [ForeignKey("Service")]
        public DateTime ServiceVersion { get; init; }
    }
}
