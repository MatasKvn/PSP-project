using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_System.Models
{
    [Table("Discount")]
    public abstract class Discount
    {
        [Key]
        public int Id { get; set; }
        public string? Description { get; set; }
        public int Amount { get; set; }
        public bool IsPercentage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        //Versioning
        public int Version { get; set; }
        public bool IsDeleted { get; set; }
    }
}
