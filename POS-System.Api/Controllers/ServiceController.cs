using Microsoft.AspNetCore.Mvc;
using POS_System.Business.Dtos.Request;
using POS_System.Business.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace POS_System.Api.Controllers;

//[Authorize]
[ApiController]
[Route("api/services")]
public class ServiceController(IServiceOfService serviceOfService) : ControllerBase
{
    [HttpGet]
    //[Authorize(Policy = "ServiceRead")]
    public async Task<IActionResult> GetAllServices(CancellationToken cancellationToken, int pageNum = 0, int pageSize = 10)
    {
        var services = await serviceOfService.GetAllServicesAsync(cancellationToken, pageNum, pageSize);

        return Ok(services);
    }

    [HttpGet("{id:int}")]
    //[Authorize(Policy = "ServiceRead")]
    public async Task<IActionResult> GetServiceById([FromRoute] int id, CancellationToken cancellationToken)
    {
        var result = await serviceOfService.GetServiceByIdAsync(id, cancellationToken);

        return Ok(result);
    }

    [HttpPost]
    //[Authorize(Policy = "ServiceWrite")]
    public async Task<IActionResult> CreateService([FromBody] ServiceRequestDto serviceRequestDto, CancellationToken cancellationToken)
    {
        var newservice = await serviceOfService.CreateServiceAsync(serviceRequestDto, cancellationToken);
        return Ok(newservice);
    }

    [HttpPut("{id:int}")]
    //[Authorize(Policy = "ServiceWrite")]
    public async Task<IActionResult> UpdateService([FromRoute] int id, [FromBody] ServiceUpdateRequestDto serviceUpdateRequestDto, CancellationToken cancellationToken)
    {
        var updatedservice = await serviceOfService.UpdateServiceAsync(id, serviceUpdateRequestDto, cancellationToken);

        return Ok(updatedservice);
    }

    [HttpDelete("{id:int}")]
    //[Authorize(Policy = "ServiceWrite")]
    public async Task<IActionResult> DeleteService([FromRoute] int id, CancellationToken cancellationToken)
    {
        await serviceOfService.DeleteServiceAsync(id, cancellationToken);

        return NoContent();
    }
}