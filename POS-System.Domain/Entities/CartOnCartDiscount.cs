using Microsoft.EntityFrameworkCore;
using POS_System.Domain.Entities.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_System.Domain.Entities
{
    [Table("CartOnCartDiscounts")]
    public record CartOnCartDiscount : BaseManyToManyEntity<Cart, CartDiscount>
    {

    }
}
