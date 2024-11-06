using Microsoft.EntityFrameworkCore;
using POS_System.Domain.Entities;

namespace POS_System.Data.Database
{
    public interface IApplicationDbContext
    {
        public DbSet<Tax> Taxes { get; set; }
    }
}
