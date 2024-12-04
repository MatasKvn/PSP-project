using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_System.Domain.Entities
{
    [Table("Services")]
    public record Service
    {
        //Primary key
        [Key]
        public int Id { get; set; }

        //Navigation properties
        public virtual ICollection<ServiceOnTax> ServiceOnTaxes { get; set; }
        public virtual ICollection<ServiceOnItemDiscount> ServiceOnItemDiscounts { get; set; }
        public virtual ICollection<EmployeeOnService> EmployeeOnServices { get; set; }

        //Fields
        [MaxLength(40)]
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required int Duration { get; set; }
        public required int Price { get; set; }
        public required string ImageURL { get; set; }

        //Versioning
        public DateTime Version { get; set; }
        public bool IsDeleted { get; set; }
    }
}
