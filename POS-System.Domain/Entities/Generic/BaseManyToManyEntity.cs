using Microsoft.EntityFrameworkCore;

namespace POS_System.Domain.Entities.Generic
{
    [PrimaryKey(nameof(LeftEntityId), nameof(RightEntityId), nameof(StartDate))]
    public abstract record BaseManyToManyEntity<TLeft, TRight>
    {
        public int LeftEntityId { get; set; }
        public int RightEntityId { get; set; }
        public DateTime StartDate { get; set; }
        public required DateTime? EndDate { get; set; }

        // Navigation properties
        public virtual TLeft LeftEntity { get; set; }
        public virtual TRight RightEntity { get; set; }
    }
}
