using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POS_System.Business.Dtos.Request;
using POS_System.Business.Services.Interfaces;

namespace POS_System.Api.Controllers
{
    [Route("api/time-slot")]
    [ApiController]
    public class TimeSlotController(ITimeSlotService _timeSlotService) : ControllerBase
    {
        [Authorize("ServiceRead")]
        [HttpGet]
        public async Task<IActionResult> GetAllTimeSlots([FromQuery] bool? onlyAvailable, CancellationToken cancellationToken, [FromQuery] int pageSize = 10, [FromQuery] int pageNumber = 0)
        {
            var timeSlots = await _timeSlotService.GetTimeSlotsAsync(pageSize, pageNumber, onlyAvailable, cancellationToken);
            return Ok(timeSlots);
        }

        [Authorize("ServiceRead")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTimeSlotById(int id, CancellationToken cancellationToken)
        {
            var timeSlot = await _timeSlotService.GetTimeSlotByIdAsync(id, cancellationToken);
            return Ok(timeSlot);
        }

        [Authorize("ServiceWrite")]
        [HttpPost]
        public async Task<IActionResult> CreateTimeSlot(TimeSlotRequest? timeSlotDto, CancellationToken cancellationToken)
        {
            var timeSlot = await _timeSlotService.CreateTimeSlotAsync(timeSlotDto, cancellationToken);
            return Ok(timeSlot);
        }

        [Authorize("ServiceWrite")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTimeSlot(int id, TimeSlotRequest? timeSlotDto, CancellationToken cancellationToken)
        {
            var timeSlot = await _timeSlotService.UpdateTimeSlotAsync(id, timeSlotDto, cancellationToken);
            return Ok(timeSlot);
        }

        [Authorize("ServiceWrite")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTimeSlot(int id, CancellationToken cancellationToken)
        {
            var timeSlot = await _timeSlotService.DeleteTimeSlotAsync(id, cancellationToken);
            return Ok(timeSlot);
        }
    }
}
