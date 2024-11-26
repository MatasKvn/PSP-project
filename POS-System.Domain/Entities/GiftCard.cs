using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace POS_System.Domain.Entities
{
    [Table("GiftCards")]
    public record GiftCard
    {
        //Primary key
        [Key]
        public int Id { get; set; }

        //Fields
        public required DateOnly Date { get; init; }
        public required int Value { get; init; }

        [MaxLength(8)]
        public required string Code { get; set; }
    }
}
