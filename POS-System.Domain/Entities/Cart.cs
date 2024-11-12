using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace POS_System.Domain.Entities
{
    [Table("Carts")]
    public record Cart
    {
        [Key]
        public int Id { get; init; }
        [ForeignKey("Employee")]
        public int EmployeeId { get; init; }
        [ForeignKey("Employee")]
        public DateTime EmployeeVersion { get; init; }
        public DateTime DateCreated { get; init; }
    }
}
