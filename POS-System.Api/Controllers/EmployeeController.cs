using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POS_System.Business.Dtos.Request;
using POS_System.Business.Services.Interfaces;

namespace POS_System.Api.Controllers
{
    [ApiController]
    [Route("api/employees")] 
    public class EmployeeController(IEmployeeeService employeeeService) : ControllerBase
    {
        //[Authorize("EmployeesRead")]
        [HttpGet]
        public async Task<IActionResult> GetEmployeesAsync([FromQuery] bool? onlyActive, CancellationToken cancellationToken, [FromQuery] int pageNumber = 0, [FromQuery] int pageSize = 10)
        {
            var result = await employeeeService.GetEmployeesAsync(pageSize, pageNumber, onlyActive, cancellationToken);

            return Ok(result);
        }

        //[Authorize("EmployeesRead")]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetEmployeeByIdAsync([FromRoute] int id, CancellationToken cancellationToken)
        {
            var result = await employeeeService.GetEmployeeByIdAsync(id, cancellationToken);

            Console.WriteLine($"Employee Retrieved: ID: {id}, First Name: {result.FirstName}, Last Name: {result.LastName}, Username: {result.UserName}, Email: {result.Email}, Phone: {result.PhoneNumber}, Birth Date: {result.BirthDate}");
            return Ok(result);
        }

        //[Authorize("EmployeesWrite")]
        [HttpPut("{employeeId:int}")]
        public async Task<IActionResult> UpdateEmployeeByIdAsync([FromRoute] int employeeId, [FromBody] EmployeeRequest employeeRequest, CancellationToken cancellationToken)
        {
            Console.WriteLine($"employeeRequest000BirthDate: {employeeRequest.BirthDate}");

            var result = await employeeeService.UpdateEmployeeByIdAsync(employeeId, employeeRequest, cancellationToken);

            return Ok(result);
        }

        //[Authorize("EmployeesWrite")]
        [HttpDelete("{employeeId:int}")]
        public async Task<IActionResult> DeleteEmployeeByIdAsync([FromRoute] int employeeId, CancellationToken cancellationToken)
        {
            var result = await employeeeService.DeleteEmployeeByIdAsync(employeeId, cancellationToken);

            return Ok(result);
        }
    }
}