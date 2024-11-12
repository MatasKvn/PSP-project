using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace POS_System.Data.Identity
{
    public class ApplicationUser<TKey> : IdentityUser<TKey>
        where TKey : IEquatable<TKey>
    {
        [MaxLength(30)]
        public required string FirstName { get; init; }
        [MaxLength(30)]
        public required string LastName { get; init; }
        public DateOnly BirthDate { get; init; }
        public DateOnly StartDate { get; init; }
        public DateOnly? EndDate { get; init; }

        //Versioning
        public DateTime Version { get; init; }
        public bool IsDeleted { get; init; }
    }
}