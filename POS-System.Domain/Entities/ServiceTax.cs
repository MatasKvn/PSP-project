using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace POS_System.Domain.Entities
{
    [Table("ServiceTax")]
    public record ServiceTax(int serviceId, DateTime serviceVersion, int taxId, DateTime taxVersion)
    {
        [Key, Column(Order = 0)]
        [ForeignKey("Service")]
        public int ServiceId { get; init; } = serviceId;
        [Key, Column(Order = 1)]
        [ForeignKey("Service")]
        public DateTime ServiceVersion { get; init; } = serviceVersion;
        [Key, Column(Order = 2)]
        [ForeignKey("Tax")]
        public int TaxId { get; init; } = taxId;
        [Key, Column(Order = 3)]
        [ForeignKey("Tax")]
        public DateTime TaxVersion { get; init; } = taxVersion;
    }
}
