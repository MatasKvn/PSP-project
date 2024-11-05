using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_System.Domain.Entities
{
    [Table("Taxes")]
    public class Tax(int id, string name, int rate, int version, bool isPercentage = true, bool isDeleted = false)
    {
        [Key]
        public int Id { get; } = id;
        [Required]
        [MaxLength(64)]
        public string Name { get; } = name;
        [Required]
        public int Rate { get; } = rate;
        [Required]
        public bool IsPercentage { get; } = isPercentage;

        //Versioning
        [Required]
        public int Version { get; } = version;
        [Required]
        public bool IsDeleted { get; } = isDeleted;
    }
}
