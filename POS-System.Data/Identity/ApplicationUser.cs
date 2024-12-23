using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace POS_System.Data.Identity
{
    public class ApplicationUser : IdentityUser<int>
    {
        public required int EmployeeId { get; set; } 
        [MaxLength(30)]
        public required string FirstName { get; set; }
        [MaxLength(30)]
        public required string LastName { get; set; }
        public DateOnly BirthDate { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public required int RoleId { get; set; }

        //Versioning
        public DateTime Version { get; set; }
        public bool IsDeleted { get; set; }
    }
}