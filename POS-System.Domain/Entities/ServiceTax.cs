using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace POS_System.Domain.Entities
{
    [Table("ServiceTax")]
    [PrimaryKey(nameof(ServiceId), nameof(ServiceVersion), nameof(TaxId), nameof(TaxVersion))]
    public record ServiceTax
    {
        [ForeignKey("Service")]
        public int ServiceId { get; init; }
        [ForeignKey("Service")]
        public DateTime ServiceVersion { get; init; }
        [ForeignKey("Tax")]
        public int TaxId { get; init; }
        [ForeignKey("Tax")]
        public DateTime TaxVersion { get; init; }
    }
}
