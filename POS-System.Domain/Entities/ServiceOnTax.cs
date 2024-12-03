using Microsoft.EntityFrameworkCore;
using POS_System.Domain.Entities.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_System.Domain.Entities
{
    [Table("ServiceOnTax")]
    public record ServiceOnTax : BaseManyToManyEntity<Service, Tax>
    {

    }
}
