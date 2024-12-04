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
    public class ServiceReservationService(IUnitOfWork _unitOfWork, IMapper _mapper) : IServiceReservationService
    {
        public async Task<ServiceReservationResponse> CreateServiceReservationAsync(ServiceReservationRequest? serviceReservationDto, CancellationToken cancellationToken)
        {
            if (serviceReservationDto is null)
            {
                throw new BadRequestException(ApplicationMessages.BAD_REQUEST_MESSAGE);
            }

            var serviceReservation = _mapper.Map<ServiceReservation>(serviceReservationDto);

            await _unitOfWork.ServiceReservationRepository.CreateAsync(serviceReservation, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var responseDto = _mapper.Map<ServiceReservationResponse>(serviceReservation);
            return responseDto;
        }

        public async Task<PagedResponse<ServiceReservationResponse?>> GetServiceReservationsAsync(int pageSize, int pageNumber, CancellationToken cancellationToken)
        {
            var (serviceReservations, totalCount) = await _unitOfWork.ServiceReservationRepository.GetAllWithPaginationAsync(
                pageSize,
                pageNumber,
                cancellationToken
            );

            if (serviceReservations is null)
            {
                throw new NotFoundException(ApplicationMessages.NOT_FOUND_ERROR);
            }

            var serviceReservationDtos = _mapper.Map<List<ServiceReservationResponse>>(serviceReservations);
            return new PagedResponse<ServiceReservationResponse?>(totalCount, pageSize, pageNumber, serviceReservationDtos);
        }

        public async Task<ServiceReservationResponse?> GetServiceReservationByIdAsync(int id, CancellationToken cancellationToken)
        {
            var serviceReservation = await _unitOfWork.ServiceReservationRepository.GetByIdAsync(id, cancellationToken);

            if (serviceReservation is null)
            {
                throw new NotFoundException(ApplicationMessages.NOT_FOUND_ERROR);
            }

            var serviceReservationDto = _mapper.Map<ServiceReservationResponse>(serviceReservation);
            return serviceReservationDto;
        }

        public async Task<ServiceReservationResponse> UpdateServiceReservationByIdAsync(int id, ServiceReservationRequest? serviceReservationDto, CancellationToken cancellationToken)
        {
            if (serviceReservationDto is null)
            {
                throw new BadRequestException(ApplicationMessages.BAD_REQUEST_MESSAGE);
            }

            var serviceReservation = await _unitOfWork.ServiceReservationRepository.GetByIdAsync(id, cancellationToken);

            if (serviceReservation is null)
            {
                throw new NotFoundException(ApplicationMessages.NOT_FOUND_ERROR);
            }

            serviceReservation.CartItemId = serviceReservationDto.CartItemId;
            serviceReservation.TimeSlotId = serviceReservationDto.TimeSlotId;
            serviceReservation.BookingTime = serviceReservationDto.BookingTime;
            serviceReservation.CustomerName = serviceReservationDto.CustomerName;
            serviceReservation.CustomerPhone = serviceReservationDto.CustomerPhone;

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var responseDto = _mapper.Map<ServiceReservationResponse>(serviceReservation);
            return responseDto;
        }
    }
}
