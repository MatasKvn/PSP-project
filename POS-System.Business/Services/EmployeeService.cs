using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using POS_System.Business.Dtos;
using POS_System.Business.Dtos.Request;
using POS_System.Business.Dtos.Response;
using POS_System.Business.Services.Interfaces;
using POS_System.Common.Constants;
using POS_System.Common.Enums;
using POS_System.Common.Exceptions;
using POS_System.Data.Identity;
using POS_System.Data.Repositories.Interfaces;
using System.Data;
using System.Linq.Expressions;

namespace POS_System.Business.Services
{
    public class EmployeeService(
        IUnitOfWork unitOfWork, 
        UserManager<ApplicationUser> userManager,
        IMapper mapper
    ) : IEmployeeeService
    {
        public async Task<PagedResponse<EmployeeResponse>> GetEmployeesAsync(int pageSize, int pageNumber, bool? onlyActive, CancellationToken cancellationToken)
        {
            var (response, totalCount) = await unitOfWork.EmployeeRepository.GetByExpressionWithPaginationAsync(
                onlyActive is null ? null : r => r.IsDeleted != onlyActive,
                pageSize,
                pageNumber,
                cancellationToken
            );


            foreach (var employee in response)
            {
                var employeeRoles = await userManager.GetRolesAsync(employee);

                if (employeeRoles == null || !employeeRoles.Any())
                {
                    continue;
                }

                employee.RoleId = GetRoleId(employeeRoles.First());
            }

            var employees = mapper.Map<IReadOnlyList<EmployeeResponse>>(response);
            return new PagedResponse<EmployeeResponse>(totalCount, pageSize, pageNumber, employees);
        }

        public async Task<EmployeeResponse> GetEmployeeByIdAsync(int employeeId, CancellationToken cancellationToken)
        {
            var employee = await unitOfWork.EmployeeRepository.GetByIdAsync(employeeId, cancellationToken);

            if (employee is null || employee.IsDeleted)
                throw new NotFoundException(ApplicationMessages.NOT_FOUND_ERROR);

            var employeeRoles = await userManager.GetRolesAsync(employee);
            if (employeeRoles.Any())
            {
                employee.RoleId = GetRoleId(employeeRoles.First());
            }
            else
            {
                employee.RoleId = (int)AccesibilityEnum.NONE;
            }

            return mapper.Map<EmployeeResponse>(employee);
        }

        public async Task<EmployeeResponse> UpdateEmployeeByIdAsync(int employeeId, EmployeeRequest employeeRequest, CancellationToken cancellationToken)
        {
            var employee = await unitOfWork.EmployeeRepository.GetByIdAsync(employeeId, cancellationToken);

            if (employee is null || employee.IsDeleted)
                throw new NotFoundException(ApplicationMessages.NOT_FOUND_ERROR);

            string newRole = GetRoleById(employeeRequest.RoleId);

            var employeeRoles = await userManager.GetRolesAsync(employee);
            string oldRole = employeeRoles.FirstOrDefault() ?? "None";

            var updatedEmployee = mapper.Map(employeeRequest, employee);
            
            await userManager.RemoveFromRoleAsync(employee, oldRole);
            await userManager.AddToRoleAsync(employee, newRole);

            var response = await userManager.UpdateAsync(updatedEmployee);

            if (!response.Succeeded)
            {
                var errors = JsonConvert.SerializeObject(response.Errors);
                throw new BadRequestException(errors);
            }

            return mapper.Map<EmployeeResponse>(updatedEmployee);
        }

        public async Task<EmployeeResponse> DeleteEmployeeByIdAsync(int employeeId, CancellationToken cancellationToken)
        {
            var employee = await unitOfWork.EmployeeRepository.GetByIdAsync(employeeId, cancellationToken);
            
            if (employee is null || employee.IsDeleted)
                throw new NotFoundException(ApplicationMessages.NOT_FOUND_ERROR);

            employee.IsDeleted = true;
            employee.EndDate = DateOnly.FromDateTime(DateTime.UtcNow);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return mapper.Map<EmployeeResponse>(employee);
        }

        private int GetRoleId(string role)
        {
            return role switch
            {
                "Super admin" => (int)AccesibilityEnum.SUPER_ADMIN,
                "Service provider" => (int)AccesibilityEnum.SERVICE_PROVIDER,
                "Cashier" => (int)AccesibilityEnum.CASHIER,
                "Owner" => (int)AccesibilityEnum.OWNER,
                "None" => (int)AccesibilityEnum.NONE,
                _ => (int)AccesibilityEnum.NONE 
            };
        }

        private string GetRoleById(int roleId)
        {
            return roleId switch
            {
                (int)AccesibilityEnum.SUPER_ADMIN => "Super admin",
                (int)AccesibilityEnum.SERVICE_PROVIDER => "Service provider",
                (int)AccesibilityEnum.CASHIER => "Cashier",
                (int)AccesibilityEnum.OWNER => "Owner",
                (int)AccesibilityEnum.NONE => "None",
                _ => "None"
            };
        }
    }
}