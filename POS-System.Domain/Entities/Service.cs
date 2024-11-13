using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_System.Domain.Entities
{
    [Table("Services")]
    public record Service
    {
        //Primary key
        [Key]
        public int Id { get; init; }

        //Navigation properties
        public virtual ICollection<ServiceOnTax> ServiceOnTaxes { get; set; }
        public virtual ICollection<ServiceOnItemDiscount> ServiceOnItemDiscounts { get; set; }
        public virtual ICollection<EmployeeOnService> EmployeeOnServices { get; set; }

        //Fields
        public int ServiceId { get; init; }
        [MaxLength(40)]
        public required string Name { get; init; }
        public required string Description { get; init; }
        public required int Duration { get; init; }
        public required int Price { get; init; }
        public required string ImageURL { get; init; }

        //Versioning
        public DateTime Version { get; init; }
        public bool IsDeleted { get; init; }
    }
}
