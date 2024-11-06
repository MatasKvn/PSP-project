using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace POS_System.Domain.Entities
{
    [Table("Carts")]
    public record Cart(int id, int employeeId, DateTime employeeVersion, DateTime dateCreated)
    {
        [Key]
        public int Id { get; init; } = id;
        [ForeignKey("Employee")]
        public int EmployeeId { get; init; } = employeeId;
        [ForeignKey("Employee")]
        public DateTime EmployeeVersion { get; init; } = employeeVersion;
        public DateTime DateCreated { get; init; } = dateCreated;
    }
}
