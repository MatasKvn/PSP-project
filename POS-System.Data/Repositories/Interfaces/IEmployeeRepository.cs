using POS_System.Data.Identity;
using POS_System.Data.Repositories.Base;

namespace POS_System.Data.Repositories.Interfaces;

public interface IEmployeeRepository : IRepository<ApplicationUser<int>>
{
}