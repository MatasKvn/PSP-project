using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_System.Models
{
    [Table("Cart")]
    public class Cart
    {
        [Key]
        public int Id { get; set; }

        public DateTime DateCreated { get; private set; }

        public Cart(int id)
        {
            this.Id = id;
            this.DateCreated = DateTime.Now;
        }
    }
}
