using MedicalCenter.Data.Repositories.Base;
using POS_System.Data.Database;
using POS_System.Data.Repositories.Interfaces;
using POS_System.Domain.Entities;

namespace POS_System.Data.Repositories;

public class EmployeeRepository(ApplicationDbContext dbContext) : Repository<Employee>(dbContext), IEmployeeRepository
{
}