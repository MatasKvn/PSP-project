using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_System.Domain.Entities
{
    [Table("CartDiscounts")]
    public class CartDiscount(int id, int value, string description, DateTime startDate, DateTime endDate, bool isPercentage = true)
    {
        [Key]
        public int Id { get; } = id;
        [Required]
        public int value { get; } = value;
        [Required]
        public bool IsPercentage { get; } = isPercentage;
        [Required]
        public string Description { get; } = description;
        [Required]
        public DateTime StartDate { get; } = startDate;
        [Required]
        public DateTime EndDate { get; } = endDate;
    }
}
