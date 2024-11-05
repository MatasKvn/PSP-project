using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_System.Domain.Entities
{
    [Table("CartItems")]
    public class CartItem(int id, int cartId, int itemId, bool isProduct = true, int quantity = 1)
    {
        [Key]
        public int Id { get; } = id;
        [Required]
        public int CartId { get; } = cartId;
        [Required]
        public int Quantity { get; } = quantity;
        [Required]
        public int ItemId { get; } = itemId;
        [Required]
        public bool IsProduct { get; } = isProduct;
    }
}
