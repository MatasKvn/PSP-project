using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace POS_System.Domain.Entities
{
    [Table("Carts")]
    public record Cart(int id, int employeeId, DateTime employeeVersion, DateTime dateCreated)
    {
        [Key]
        public int Id { get; init; } = id;
        public int EmployeeId { get; init; } = employeeId;
        public DateTime EmployeeVersion { get; init; } = employeeVersion;
        public DateTime DateCreated { get; init; } = dateCreated;
    }
}
