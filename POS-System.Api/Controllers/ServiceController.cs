using Microsoft.AspNetCore.Mvc;
using POS_System.Business.Dtos.Request;
using POS_System.Business.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using POS_System.Business.Services;

namespace POS_System.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/services")]
public class ServiceController(IServiceOfService _serviceOfService) : ControllerBase
{
    [HttpGet]
    [Authorize(Policy = "ServiceRead")]
    public async Task<IActionResult> GetAllServices(CancellationToken cancellationToken, int pageNum = 0, int pageSize = 10)
    {
        var services = await _serviceOfService.GetAllServicesAsync(cancellationToken, pageNum, pageSize);

        return Ok(services);
    }

    [HttpGet("{id:int}")]
    [Authorize(Policy = "ServiceRead")]
    public async Task<IActionResult> GetServiceById([FromRoute] int id, CancellationToken cancellationToken)
    {
        var result = await _serviceOfService.GetServiceByIdAsync(id, cancellationToken);

        return Ok(result);
    }

    [HttpPost]
    [Authorize(Policy = "ServiceWrite")]
    public async Task<IActionResult> CreateService([FromBody] ServiceRequest serviceRequest, CancellationToken cancellationToken)
    {
        var newservice = await _serviceOfService.CreateServiceAsync(serviceRequest, cancellationToken);
        return Ok(newservice);
    }

    [HttpPut("{id:int}")]
    [Authorize(Policy = "ServiceWrite")]
    public async Task<IActionResult> UpdateService([FromRoute] int id, [FromBody] ServiceRequest serviceRequest, CancellationToken cancellationToken)
    {
        var updatedservice = await _serviceOfService.UpdateServiceAsync(id, serviceRequest, cancellationToken);

        return Ok(updatedservice);
    }

    [HttpDelete("{id:int}")]
    [Authorize(Policy = "ServiceWrite")]
    public async Task<IActionResult> DeleteService([FromRoute] int id, CancellationToken cancellationToken)
    {
        await _serviceOfService.DeleteServiceAsync(id, cancellationToken);

        return NoContent();
    }


    [HttpPut("{id}/link")]
    [Authorize(Policy = "ServiceWrite")]
    public async Task<IActionResult> LinkServiceToTaxes(int id, [FromBody] int[] taxIdList, CancellationToken cancellationToken)
    {
        await _serviceOfService.LinkServiceToTaxesAsync(id, taxIdList, cancellationToken);

        return Ok();
    }

    [HttpPut("{id}/unlink")]
    [Authorize(Policy = "ServiceRead")]
    public async Task<IActionResult> UnlinkServiceFromTaxes(int id, [FromBody] int[] taxIdList, CancellationToken cancellationToken)
    {
        await _serviceOfService.UnlinkServiceFromTaxesAsync(id, taxIdList, cancellationToken);

        return Ok();
    }

    //Leave timeStamp null if you want to get only the active items
    [HttpGet("item/{id}")]
    [Authorize(Policy = "ItemRead")]
    public async Task<IActionResult> GetServicesLinkedToTaxId(int id, [FromQuery] DateTime? timeStamp, CancellationToken cancellationToken)
    {
        var services = await _serviceOfService.GetServicesLinkedToTaxId(id, timeStamp, cancellationToken);
        return Ok(services);
    }
}