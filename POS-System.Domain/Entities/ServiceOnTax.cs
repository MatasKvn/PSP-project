using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_System.Domain.Entities
{
    [Table("ServiceOnTax")]
    [PrimaryKey(nameof(ServiceVersionId), nameof(TaxVersionId), nameof(StartDate))]
    public record ServiceOnTax
    {
        //Priamry key
        [ForeignKey("Service")]
        public int ServiceVersionId { get; set; }
        [ForeignKey("Tax")]
        public int TaxVersionId { get; set; }
        public DateTime StartDate { get; set; }
        public required DateTime? EndDate { get; set; }

        //Navigation properties
        public virtual Service Service { get; set; }
        public virtual Tax Tax { get; set; }
    }
}
