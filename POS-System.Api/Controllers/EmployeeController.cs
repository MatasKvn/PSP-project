using Microsoft.AspNetCore.Mvc;
using POS_System.Business.Dtos.Request;
using POS_System.Business.Services.Interfaces;

namespace POS_System.Api.Controllers
{

    [ApiController]
    [Route("/v1/employee")] 
    public class EmployeeController(IEmployeeeService employeeeService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetEmployeesAsync([FromQuery] int pageSize, [FromQuery] int pageNumber, [FromQuery] bool? onlyActive, CancellationToken cancellationToken)
        {
            var result = await employeeeService.GetEmployeesAsync(pageSize, pageNumber, onlyActive, cancellationToken);

            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetEmployeeByIdAsync([FromRoute] int id, CancellationToken cancellationToken)
        {
            var result = await employeeeService.GetEmployeeByIdAsync(id, cancellationToken);

            return Ok(result);
        }

        [HttpPut("{employeeId:int}")]
        public async Task<IActionResult> UpdateEmployeeByIdAsync([FromRoute] int employeeId, [FromBody] EmployeeRequest employeeRequest, CancellationToken cancellationToken)
        {
            var result = await employeeeService.UpdateEmployeeByIdAsync(employeeId, employeeRequest, cancellationToken);

            return Ok(result);
        }

        [HttpDelete("{employeeId:int}")]
        public async Task<IActionResult> DeleteEmployeeByIdAsync([FromRoute] int employeeId, CancellationToken cancellationToken)
        {
            var result = await employeeeService.DeleteEmployeeByIdAsync(employeeId, cancellationToken);

            return Ok(result);
        }
    }
}