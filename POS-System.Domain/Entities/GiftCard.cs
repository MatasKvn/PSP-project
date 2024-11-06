using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace POS_System.Domain.Entities
{
    [Table("GiftCards")]
    public record GiftCard(int id, DateTime date, int value, string code)
    {
        [Key]
        public int Id { get; set; } = id;
        public DateTime Date { get; set; } = date;
        public int Value { get; set; } = value;
        [MaxLength(8)]
        public string Code { get; set; } = code;
    }
}
