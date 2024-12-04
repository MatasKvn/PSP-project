using POS_System.Domain.Entities.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_System.Domain.Entities
{
    [Table("ProductOnItemDiscounts")]
    public record ProductOnItemDiscount : BaseManyToManyEntity<Product, ItemDiscount>
    {

    }
}
