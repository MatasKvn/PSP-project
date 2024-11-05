using POS_System.Common.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_System.Domain.Entities
{
    [Table("Employees")]
    public class Employee(int id, string firstName, string lastName, string email, string phoneNumber, string password,
            DateOnly birthDate, DateOnly startDate, AccesibilityEnum accesibility, int version, DateOnly? endDate = null, bool isDeleted = false)
    {
        [Key]
        public int Id { get; } = id;
        [Required]
        [MaxLength(30)]
        public string FirstName { get; } = firstName;
        [Required]
        [MaxLength(30)]
        public string LastName { get; } = lastName; 
        [Required]
        public string Email { get; set; } = email;
        [Required]
        [MaxLength(15)]
        public string PhoneNumber { get; } = phoneNumber;
        [Required]
        public string Password { get; } = password;
        [Required]
        public DateOnly BirthDate { get; } = birthDate;
        [Required]
        public DateOnly StartDate { get; } = startDate;
        public DateOnly? EndDate { get; } = endDate;
        [Required]
        public AccesibilityEnum Accesibility { get; } = accesibility;

        //Versioning
        [Required]
        public int Version { get; } = version;
        [Required]
        public bool IsDeleted { get; } = isDeleted;

        //Authentication, I think we said we will use Identity so this might be usseless
        [Required]
        public byte[] PasswordHash { get; }
    }
}
