using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_System.Domain.Entities
{
    [Table("Taxes")]
    public record Tax(int id, string name, int rate, DateTime version, bool isPercentage = true, bool isDeleted = false)
    {
        [Key, Column(Order = 0)]
        public int Id { get; init; } = id;
        [MaxLength(64)]
        public string Name { get; init; } = name;
        public int Rate { get; init; } = rate;
        public bool IsPercentage { get; init; } = isPercentage;

        //Versioning
        [Key, Column(Order = 1)]
        public DateTime Version { get; init; } = version;
        public bool IsDeleted { get; init; } = isDeleted;
    }
}
