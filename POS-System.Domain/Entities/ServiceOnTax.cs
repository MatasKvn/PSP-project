using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_System.Domain.Entities
{
    [Table("ServiceOnTax")]
    [PrimaryKey(nameof(ServiceVersionId), nameof(TaxVersionId))]
    public record ServiceOnTax
    {
        //Priamry key
        [ForeignKey("Service")]
        public int ServiceVersionId { get; init; }
        [ForeignKey("Tax")]
        public int TaxVersionId { get; init; }

        //Navigation properties
        public virtual Service Service { get; set; }
        public virtual Tax Tax { get; set; }
    }
}
