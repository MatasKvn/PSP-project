using Microsoft.AspNetCore.Mvc;
using POS_System.Business.Dtos.TimeSlotDtos;
using POS_System.Business.Services.Interfaces;

namespace POS_System.Api.Controllers
{
    [Route("api/time-slot")]
    [ApiController]
    public class TimeSlotController(ITimeSlotService _timeSlotService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllTimeSlots(CancellationToken cancellationToken)
        {
            var timeSlots = await _timeSlotService.GetTimeSlotsAsync(cancellationToken);
            return Ok(timeSlots);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTimeSlotById(int id, CancellationToken cancellationToken)
        {
            var timeSlot = await _timeSlotService.GetTimeSlotByIdAsync(id, cancellationToken);
            return Ok(timeSlot);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTimeSlot(CreateTimeSlotDto? timeSlotDto, CancellationToken cancelationToken)
        {
            var timeSlot = await _timeSlotService.CreateTimeSlotAsync(timeSlotDto, cancelationToken);
            return Ok(timeSlot);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTimeSlot(int id, CreateTimeSlotDto? timeSlotDto, CancellationToken cancellationToken)
        {
            var timeSlot = await _timeSlotService.UpdateTimeSlotAsync(id, timeSlotDto, cancellationToken);
            return Ok(timeSlot);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTimeSlot(int id, CancellationToken cancellationToken)
        {
            var timeSlot = await _timeSlotService.DeleteTimeSlotAsync(id, cancellationToken);
            return Ok(timeSlot);
        }
    }
}
