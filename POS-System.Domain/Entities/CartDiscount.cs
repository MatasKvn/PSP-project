using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_System.Domain.Entities
{
    [Table("CartDiscounts")]
    [PrimaryKey(nameof(Id), nameof(Version))]
    public record CartDiscount
    {
        public int Id { get; init; }
        public int Value { get; init; }
        public bool IsPercentage { get; init; }
        public required string Description { get; init; }
        public DateTime StartDate { get; init; }
        public DateTime EndDate { get; init; }

        //Versioning
        public DateTime Version { get; init; }
        public bool IsDeleted { get; init; }
    }
}
