using POS_System.Business.Dtos.Request;
using POS_System.Business.Dtos.Response;

namespace POS_System.Business.Services.Interfaces
{
    public interface ITimeSlotService
    {
        public Task<IEnumerable<TimeSlotResponse?>> GetTimeSlotsAsync(CancellationToken cancellationToken);
        public Task<TimeSlotResponse?> GetTimeSlotByIdAsync(int id, CancellationToken cancellationToken);
        public Task<TimeSlotResponse> CreateTimeSlotAsync(TimeSlotRequest? timeSlitDto, CancellationToken cancellationToken);
        public Task<TimeSlotResponse> UpdateTimeSlotAsync(int id, TimeSlotRequest? timeSlotDto, CancellationToken cancellationToken);
        public Task<TimeSlotResponse> DeleteTimeSlotAsync(int id, CancellationToken cancellationToken);
    }
}
