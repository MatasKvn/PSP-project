using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_System.Models
{
    [Table("TimeSlot")]
    public class TimeSlot
    {
        [Key]
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public bool IsAvailable { get; set; }

        public TimeSlot(int id, DateTime startDate, bool isAvailable = true)
        {
            Id = id;
            StartDate = startDate;
            IsAvailable = isAvailable;
        }
    }
}
