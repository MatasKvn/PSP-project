using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_System.Domain.Entities
{
    [Table("EmployeeOnServices")]
    [PrimaryKey(nameof(EmployeeVersionId), nameof(ServiceVersionId))]
    public record EmployeeOnService
    {
        //Primary key
        [ForeignKey("Employee")]
        public int EmployeeVersionId { get; init; }
        [ForeignKey("Service")]
        public int ServiceVersionId { get; init; }

        //Navigation property
        public virtual Service Service { get; set; }
    }
}
