using POS_System.Data.Repositories.Base;
using POS_System.Data.Database;
using POS_System.Data.Identity;
using POS_System.Data.Repositories.Interfaces;

namespace POS_System.Data.Repositories;

public class EmployeeRepository(ApplicationDbContext dbContext) : Repository<ApplicationUser>(dbContext), IEmployeeRepository
{
}