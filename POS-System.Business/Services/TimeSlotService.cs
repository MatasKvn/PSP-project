using AutoMapper;
using POS_System.Business.Dtos.TimeSlotDtos;
using POS_System.Business.Services.Interfaces;
using POS_System.Data.Repositories.Interfaces;
using POS_System.Domain.Entities;

namespace POS_System.Business.Services
{
    public class TimeSlotService(IUnitOfWork _unitOfWork, IMapper _mapper) : ITimeSlotService
    {
        public async Task<GetTimeSlotDto> CreateTimeSlotAsync(CreateTimeSlotDto? timeSlitDto, CancellationToken cancellationToken)
        {
            var timeSlot = _mapper.Map<TimeSlot>(timeSlitDto);

            await _unitOfWork.TimeSlotRepository.CreateAsync(timeSlot, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var responseTimeSlotDto = _mapper.Map<GetTimeSlotDto>(timeSlot);
            return responseTimeSlotDto;
        }

        public async Task<GetTimeSlotDto> DeleteTimeSlotAsync(int id, CancellationToken cancellationToken)
        {
            var timeSlot = await _unitOfWork.TimeSlotRepository.GetByIdAsync(id, cancellationToken);

            timeSlot.IsAvailable = false;

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var responseTimeSlotDto = _mapper.Map<GetTimeSlotDto>(timeSlot);
            return responseTimeSlotDto;
        }

        public async Task<GetTimeSlotDto?> GetTimeSlotByIdAsync(int id, CancellationToken cancellationToken)
        {
            var timeSlot = await _unitOfWork.TimeSlotRepository.GetByIdAsync(id, cancellationToken);

            var timeSlotDto = _mapper.Map<GetTimeSlotDto>(timeSlot);

            return timeSlotDto;
        }

        public async Task<IEnumerable<GetTimeSlotDto?>> GetTimeSlotsAsync(CancellationToken cancellationToken)
        {
            var timeSlots = await _unitOfWork.TimeSlotRepository.GetAllByExpressionAsync(x => x.IsAvailable, cancellationToken);

            var timeSlotDtos = _mapper.Map<List<GetTimeSlotDto>>(timeSlots);

            return timeSlotDtos;
        }

        public async Task<GetTimeSlotDto> UpdateTimeSlotAsync(int id, CreateTimeSlotDto? timeSlotDto, CancellationToken cancellationToken)
        {
            var timeSlot = await _unitOfWork.TimeSlotRepository.GetByIdAsync(id, cancellationToken);

            timeSlot.EmployeeVersionId = timeSlotDto.EmployeeVersionId;
            timeSlot.StartTime = timeSlotDto.StartTime;
            timeSlot.IsAvailable = timeSlotDto.IsAvailable;

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var responseTimeSlotDto = _mapper.Map<GetTimeSlotDto>(timeSlot);
            return responseTimeSlotDto;
        }
    }
}
