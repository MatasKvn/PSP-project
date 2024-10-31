using POS_System.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_System.Models
{
    [Table("Employee")]
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public DateOnly BirthDate { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public AccesibilityEnum Accesibility { get; set; }

        //Versioning
        public int Version { get; set; }
        public bool IsDeleted { get; set; }

        //Authentication
        public byte[] PasswordHash { get; set; }

        public Employee(int id, string firstName, string lastName, string email, string phoneNumber, string password,
            DateOnly birthDate, DateOnly startDate, AccesibilityEnum accesibility, int version, DateOnly? endDate = null, bool isDeleted = false)
        {
            this.Id = id;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.PhoneNumber = phoneNumber;
            this.Password = password;
            this.BirthDate = birthDate;
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.Accesibility = accesibility;
            this.Version = version;
            this.IsDeleted = isDeleted;
            //this.PasswordHash = Hash(password) Do we hash the password here?
        }
    }
}
