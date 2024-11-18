using POS_System.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace POS_System.Business.Dtos
{
    public class TaxDto
    {
        //Primary key
        [Key]
        public int Id { get; init; }

        //Fields
        public int TaxId { get; init; }
        [MaxLength(64)]
        public required string Name { get; init; }
        public required int Rate { get; init; }
        public required bool IsPercentage { get; init; }

        //Versioning
        public DateTime Version { get; init; }
        public bool IsDeleted { get; init; }
    }
}
