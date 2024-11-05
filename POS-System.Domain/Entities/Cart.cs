using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace POS_System.Domain.Entities
{
    [Table("Carts")]
    public class Cart(int id, int employeeId, DateTime dateCreated)
    {
        [Key]
        public int Id { get; } = id;
        [Required]
        public int EmployeeId { get; } = employeeId;
        [Required]
        public DateTime DateCreated { get; } = dateCreated;
    }
}
