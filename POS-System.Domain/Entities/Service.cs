using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_System.Domain.Entities
{
    [Table("Services")]
    [PrimaryKey(nameof(Id), nameof(Version))]
    public record Service
    {
        public int Id { get; init; }
        [MaxLength(40)]
        public required string Name { get; init; }
        public required string Description { get; init; }
        public int Duration { get; init; }
        public int Price { get; init; }
        public required string ImageURL { get; init; }

        //Versioning
        public DateTime Version { get; init; }
        public bool IsDeleted { get; init; }
    }
}
