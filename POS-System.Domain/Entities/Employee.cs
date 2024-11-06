using POS_System.Common.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_System.Domain.Entities
{
    [Table("Employees")]
    public record Employee(int id, string firstName, string lastName, string email, string phoneNumber, string password,
            DateOnly birthDate, DateOnly startDate, AccesibilityEnum accesibility, DateTime version, DateOnly? endDate = null, bool isDeleted = false)
    {
        [Key, Column(Order = 0)]
        public int Id { get; init; } = id;
        [MaxLength(30)]
        public string FirstName { get; init; } = firstName;
        [MaxLength(30)]
        public string LastName { get; init; } = lastName; 
        public string Email { get; init; } = email;
        [MaxLength(15)]
        public string PhoneNumber { get; init; } = phoneNumber;
        public string Password { get; init; } = password;
        public DateOnly BirthDate { get; init; } = birthDate;
        public DateOnly StartDate { get; init; } = startDate;
        public DateOnly? EndDate { get; init; } = endDate;
        public AccesibilityEnum Accesibility { get; init; } = accesibility;

        //Versioning
        [Key, Column(Order = 1)]
        public DateTime Version { get; init; } = version;
        public bool IsDeleted { get; init; } = isDeleted;

        //Authentication, I think we said we will use Identity so this might be usseless
        public byte[] PasswordHash { get; init; }
    }
}
