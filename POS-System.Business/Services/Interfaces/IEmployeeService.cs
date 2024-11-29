using POS_System.Business.Dtos;
using POS_System.Business.Dtos.Request;
using POS_System.Business.Dtos.Response;

namespace POS_System.Business.Services.Interfaces
{
    public interface IEmployeeeService
    {
        Task<PagedResponse<EmployeeResponse>> GetEmployeesAsync(int pageSize, int pageNumber, bool? onlyActive, CancellationToken cancellationToken);
        Task<EmployeeResponse> GetEmployeeByIdAsync(int employeeId, CancellationToken cancellationToken);
        Task<EmployeeResponse> UpdateEmployeeByIdAsync(int employeeId, EmployeeRequest employeeRequest, CancellationToken cancellationToken);
        Task<EmployeeResponse> DeleteEmployeeByIdAsync(int employeeId, CancellationToken cancellationToken);
    } 
}