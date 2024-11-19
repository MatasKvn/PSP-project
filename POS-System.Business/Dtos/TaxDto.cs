using POS_System.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace POS_System.Business.Dtos
{
    public class TaxDto
    {
        //Primary key
        [Key]
        public int Id { get; set; }

        //Fields
        public int TaxId { get; set; }
        [MaxLength(64)]
        public required string Name { get; set; }
        public required int Rate { get; set; }
        public required bool IsPercentage { get; set; }

        //Versioning
        public DateTime Version { get; set; }
        public bool IsDeleted { get; set; }
    }
}
