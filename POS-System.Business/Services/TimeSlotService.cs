using AutoMapper;
using POS_System.Business.Dtos.Request;
using POS_System.Business.Dtos.Response;
using POS_System.Business.Services.Interfaces;
using POS_System.Data.Repositories.Interfaces;
using POS_System.Domain.Entities;

namespace POS_System.Business.Services
{
    public class TimeSlotService(IUnitOfWork _unitOfWork, IMapper _mapper) : ITimeSlotService
    {
        public async Task<TimeSlotResponse> CreateTimeSlotAsync(TimeSlotRequest? timeSlitDto, CancellationToken cancellationToken)
        {
            var timeSlot = _mapper.Map<TimeSlot>(timeSlitDto);

            await _unitOfWork.TimeSlotRepository.CreateAsync(timeSlot, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var responseTimeSlotDto = _mapper.Map<TimeSlotResponse>(timeSlot);
            return responseTimeSlotDto;
        }

        public async Task<TimeSlotResponse> DeleteTimeSlotAsync(int id, CancellationToken cancellationToken)
        {
            var timeSlot = await _unitOfWork.TimeSlotRepository.GetByIdAsync(id, cancellationToken);

            timeSlot.IsAvailable = false;

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var responseTimeSlotDto = _mapper.Map<TimeSlotResponse>(timeSlot);
            return responseTimeSlotDto;
        }

        public async Task<TimeSlotResponse?> GetTimeSlotByIdAsync(int id, CancellationToken cancellationToken)
        {
            var timeSlot = await _unitOfWork.TimeSlotRepository.GetByIdAsync(id, cancellationToken);

            var timeSlotDto = _mapper.Map<TimeSlotResponse>(timeSlot);

            return timeSlotDto;
        }

        public async Task<IEnumerable<TimeSlotResponse?>> GetTimeSlotsAsync(CancellationToken cancellationToken)
        {
            var timeSlots = await _unitOfWork.TimeSlotRepository.GetAllByExpressionAsync(x => x.IsAvailable, cancellationToken);

            var timeSlotDtos = _mapper.Map<List<TimeSlotResponse>>(timeSlots);

            return timeSlotDtos;
        }

        public async Task<TimeSlotResponse> UpdateTimeSlotAsync(int id, TimeSlotRequest? timeSlotDto, CancellationToken cancellationToken)
        {
            var timeSlot = await _unitOfWork.TimeSlotRepository.GetByIdAsync(id, cancellationToken);

            timeSlot.EmployeeVersionId = timeSlotDto.EmployeeVersionId;
            timeSlot.StartTime = timeSlotDto.StartTime;
            timeSlot.IsAvailable = timeSlotDto.IsAvailable;

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var responseTimeSlotDto = _mapper.Map<TimeSlotResponse>(timeSlot);
            return responseTimeSlotDto;
        }
    }
}
