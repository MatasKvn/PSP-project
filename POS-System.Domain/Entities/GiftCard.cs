using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace POS_System.Domain.Entities
{
    [Table("GiftCards")]
    public record GiftCard
    {
        //Primary key
        [Key]
        public required string Id { get; set; }

        //Fields
        public required DateOnly Date { get; set; }
        public required long Value { get; set; }
    }
}
