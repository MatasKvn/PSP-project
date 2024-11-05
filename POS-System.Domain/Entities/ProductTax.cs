using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_System.Domain.Entities
{
    [Table("ProductTax")]
    public class ProductTax(int productId, int taxId)
    {
        [Key, Column(Order = 0)]
        public int ProductId { get; } = productId;
        [Key, Column(Order = 1)]
        public int TaxId { get; } = taxId;
    }
}
