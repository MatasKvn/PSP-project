using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace POS_System.Domain.Entities
{
    [Table("ServiceTax")]
    [PrimaryKey(nameof(ServiceId), nameof(ServiceVersion), nameof(TaxId), nameof(TaxVersion))]
    public record ServiceTax
    {
        public int ServiceId { get; init; }
        public DateTime ServiceVersion { get; init; }
        public int TaxId { get; init; }
        public DateTime TaxVersion { get; init; }
        public Service Service { get; init; } = null!;
        public Tax Tax { get; init; } = null!;
    }
}
