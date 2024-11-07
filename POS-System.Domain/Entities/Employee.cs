using POS_System.Common.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_System.Domain.Entities
{
    [Table("Employees")]
    [PrimaryKey(nameof(Id), nameof(Version))]
    public record Employee
    {
        public int Id { get; init; }
        [MaxLength(30)]
        public required string FirstName { get; init; }
        [MaxLength(30)]
        public required string LastName { get; init; }
        public required string Email { get; init; }
        [MaxLength(15)]
        public required string PhoneNumber { get; init; }
        public required string Password { get; init; }
        public DateOnly BirthDate { get; init; }
        public DateOnly StartDate { get; init; }
        public DateOnly? EndDate { get; init; }
        public AccesibilityEnum Accesibility { get; init; }

        //Versioning
        public DateTime Version { get; init; }
        public bool IsDeleted { get; init; }

        //Authentication, I think we said we will use Identity so this might be usseless
        public required byte[] PasswordHash { get; init; }
    }
}
