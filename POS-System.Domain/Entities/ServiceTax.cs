using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace POS_System.Domain.Entities
{
    [Table("ServiceTax")]
    public class ServiceTax(int serviceId, int taxId)
    {
        [Key, Column(Order = 0)]
        public int ServiceId { get; } = serviceId;
        [Key, Column(Order = 1)]
        public int TaxId { get; } = taxId;
    }
}
