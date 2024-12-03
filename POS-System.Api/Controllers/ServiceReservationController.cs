using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POS_System.Business.Dtos.Request;
using POS_System.Business.Services.Interfaces;

namespace POS_System.Api.Controllers
{
    [Route("api/service-reservation")]
    [ApiController]
    public class ServiceReservationController(IServiceReservationService _serviceReservationService) : ControllerBase
    {
        [Authorize("ServiceRead")]
        [HttpGet]
        public async Task<IActionResult> GetAllServiceReservations(CancellationToken cancellationToken, [FromQuery] int pageSize = 10, [FromQuery] int pageNumber = 0)
        {
            var serviceReservations = await _serviceReservationService.GetServiceReservationsAsync(pageSize, pageNumber, cancellationToken);
            return Ok(serviceReservations);
        }

        [Authorize("ServiceRead")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetServiceReservationById(int id, CancellationToken cancellationToken)
        {
            var serviceReservation = await _serviceReservationService.GetServiceReservationByIdAsync(id, cancellationToken);
            return Ok(serviceReservation);
        }

        [Authorize("ServiceWrite")]
        [HttpPost]
        public async Task<IActionResult> CreateServiceReservation(ServiceReservationRequest? serviceReservationDto, CancellationToken cancellationToken)
        {
            var serviceReservation = await _serviceReservationService.CreateServiceReservationAsync(serviceReservationDto, cancellationToken);
            return Ok(serviceReservation);
        }

        [Authorize("ServiceWrite")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateServiceReservation(int id, ServiceReservationRequest? serviceReservationDto, CancellationToken cancellationToken)
        {
            var serviceReservation = await _serviceReservationService.UpdateServiceReservationByIdAsync(id, serviceReservationDto, cancellationToken);
            return Ok(serviceReservation);
        }
    }
}
