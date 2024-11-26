using POS_System.Business.Dtos.TimeSlotDtos;

namespace POS_System.Business.Services.Interfaces
{
    public interface ITimeSlotService
    {
        public Task<IEnumerable<GetTimeSlotDto?>> GetTimeSlotsAsync(CancellationToken cancellationToken);
        public Task<GetTimeSlotDto?> GetTimeSlotByIdAsync(int id, CancellationToken cancellationToken);
        public Task<GetTimeSlotDto> CreateTimeSlotAsync(CreateTimeSlotDto? timeSlitDto, CancellationToken cancellationToken);
        public Task<GetTimeSlotDto> UpdateTimeSlotAsync(int id, CreateTimeSlotDto? timeSlotDto, CancellationToken cancellationToken);
        public Task<GetTimeSlotDto> DeleteTimeSlotAsync(int id, CancellationToken cancellationToken);
    }
}
