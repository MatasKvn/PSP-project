using AutoMapper;
using POS_System.Business.Dtos;
using POS_System.Business.Dtos.Request;
using POS_System.Business.Dtos.Response;
using POS_System.Business.Services.Interfaces;
using POS_System.Common.Constants;
using POS_System.Common.Exceptions;
using POS_System.Data.Repositories.Interfaces;
using POS_System.Domain.Entities;

namespace POS_System.Business.Services
{
    public class TimeSlotService(IUnitOfWork _unitOfWork, IMapper _mapper) : ITimeSlotService
    {
        public async Task<TimeSlotResponse> CreateTimeSlotAsync(TimeSlotRequest? timeSlotDto, CancellationToken cancellationToken)
        {
            if (timeSlotDto is null)
            {
                throw new BadRequestException(ApplicationMessages.BAD_REQUEST_MESSAGE);
            }

            var timeSlot = _mapper.Map<TimeSlot>(timeSlotDto);

            await _unitOfWork.TimeSlotRepository.CreateAsync(timeSlot, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var responseTimeSlotDto = _mapper.Map<TimeSlotResponse>(timeSlot);
            return responseTimeSlotDto;
        }

        public async Task<TimeSlotResponse> DeleteTimeSlotAsync(int id, CancellationToken cancellationToken)
        {
            var timeSlot = await _unitOfWork.TimeSlotRepository.GetByIdAsync(id, cancellationToken);

            if (timeSlot is null)
            {
                throw new NotFoundException(ApplicationMessages.NOT_FOUND_ERROR);
            }

            timeSlot.IsAvailable = false;

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var responseTimeSlotDto = _mapper.Map<TimeSlotResponse>(timeSlot);
            return responseTimeSlotDto;
        }

        public async Task<TimeSlotResponse?> GetTimeSlotByIdAsync(int id, CancellationToken cancellationToken)
        {
            var timeSlot = await _unitOfWork.TimeSlotRepository.GetByIdAsync(id, cancellationToken);

            if (timeSlot is null)
            {
                throw new NotFoundException(ApplicationMessages.NOT_FOUND_ERROR);
            }

            var timeSlotDto = _mapper.Map<TimeSlotResponse>(timeSlot);

            return timeSlotDto;
        }

        public async Task<PagedResponse<TimeSlotResponse?>> GetTimeSlotsAsync(int pageSize, int pageNumber, bool? onlyAvailable, CancellationToken cancellationToken)
        {
            var (timeSlots, totalCount) = await _unitOfWork.TimeSlotRepository.GetByExpressionWithPaginationAsync(
                onlyAvailable is null ? null : r => r.IsAvailable == onlyAvailable,
                pageSize,
                pageNumber,
                cancellationToken
            );

            if (timeSlots is null)
            {
                throw new NotFoundException(ApplicationMessages.NOT_FOUND_ERROR);
            }

            var timeSlotDtos = _mapper.Map<List<TimeSlotResponse>>(timeSlots);

            return new PagedResponse<TimeSlotResponse?>(totalCount, pageSize, pageNumber, timeSlotDtos);
        }

        public async Task<TimeSlotResponse> UpdateTimeSlotAsync(int id, TimeSlotRequest? timeSlotDto, CancellationToken cancellationToken)
        {
            if (timeSlotDto is null)
            {
                throw new BadRequestException(ApplicationMessages.BAD_REQUEST_MESSAGE);
            }

            var timeSlot = await _unitOfWork.TimeSlotRepository.GetByIdAsync(id, cancellationToken);

            if (timeSlot is null)
            {
                throw new NotFoundException(ApplicationMessages.NOT_FOUND_ERROR);
            }

            timeSlot.EmployeeVersionId = timeSlotDto.EmployeeVersionId;
            timeSlot.StartTime = timeSlotDto.StartTime;
            timeSlot.IsAvailable = timeSlotDto.IsAvailable;

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var responseTimeSlotDto = _mapper.Map<TimeSlotResponse>(timeSlot);
            return responseTimeSlotDto;
        }
    }
}
