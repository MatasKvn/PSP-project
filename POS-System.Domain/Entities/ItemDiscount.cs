using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace POS_System.Domain.Entities
{
    [Table("ItemDiscounts")]
    public record ItemDiscount(int id, int value, string description, DateTime startDate, DateTime endDate, DateTime version, bool isDeleted = false, bool isPercentage = true)
    {
        [Key, Column(Order = 0)]
        public int Id { get; init; } = id;
        public int value { get; init; } = value;
        public bool IsPercentage { get; init; } = isPercentage;
        public string Description { get; init; } = description;
        public DateTime StartDate { get; init; } = startDate;
        public DateTime EndDate { get; init; } = endDate;

        //Versioning
        [Key, Column(Order = 1)]
        public DateTime Version { get; init; } = version;
        public bool IsDeleted { get; init; } = isDeleted;
    }
}
