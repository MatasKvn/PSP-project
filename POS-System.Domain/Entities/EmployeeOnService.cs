using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_System.Domain.Entities
{
    [Table("EmployeeOnServices")]
    [PrimaryKey(nameof(EmployeeVersionId), nameof(ServiceVersionId), nameof(StartDate))]
    public record EmployeeOnService
    {
        //Primary key
        [ForeignKey("Employee")]
        public int EmployeeVersionId { get; set; }
        [ForeignKey("Service")]
        public int ServiceVersionId { get; set; }
        public DateTime StartDate { get; set; }
        public required DateTime? EndDate { get; set; }

        //Navigation property
        public virtual Service Service { get; set; }
    }
}
