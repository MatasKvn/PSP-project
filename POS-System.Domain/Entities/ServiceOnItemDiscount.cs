using POS_System.Domain.Entities.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_System.Domain.Entities
{
    [Table("ServiceOnItemDiscounts")]
    public record ServiceOnItemDiscount : BaseManyToManyEntity<Service, ItemDiscount>
    {

    }
}
