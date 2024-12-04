using AutoMapper;
using POS_System.Business.Validators;
using POS_System.Business.Services.Interfaces;
using POS_System.Data.Repositories.Interfaces;
using POS_System.Domain.Entities;
using POS_System.Common.Exceptions;
using POS_System.Business.Dtos;
using POS_System.Business.Dtos.Response;
using POS_System.Business.Dtos.Request;

namespace POS_System.Business.Services
{
    public class ServiceOfSevice(IUnitOfWork unitOfWork, IMapper mapper) : IServiceOfService
    {
        public async Task<PagedResponse<ServiceResponseDto>> GetAllServicesAsync(CancellationToken cancellationToken, int pageNum, int pageSize)
        {
            var (services, totalCount) = await unitOfWork.ServiceRepository.GetAllWithPaginationAsync(
                pageSize,
                pageNum,
            cancellationToken
            );

            var mappedservices = mapper.Map<IEnumerable<ServiceResponseDto>>(services);
            return new PagedResponse<ServiceResponseDto>(totalCount, pageSize, pageNum, mappedservices);
        }

        public async Task<ServiceResponseDto?> GetServiceByIdAsync(int id, CancellationToken cancellationToken)
        {
            IdValidator.ValidateId(id);

            var service = await unitOfWork.ServiceRepository.GetByExpressionAsync(
                x => x.Id == id && !x.IsDeleted,cancellationToken) 
                    ?? throw new NotFoundException($"Service with id {id} does not exist.");

            return mapper.Map<ServiceResponseDto>(service);
        }

        public async Task<ServiceResponseDto> CreateServiceAsync(ServiceRequestDto serviceRequestDto, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(serviceRequestDto, nameof(serviceRequestDto));

            var newService = mapper.Map<Service>(serviceRequestDto);

            newService.Version = DateTime.Now;
            newService.IsDeleted = false;

            await unitOfWork.ServiceRepository.CreateAsync(newService, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return mapper.Map<ServiceResponseDto>(newService);
        }

        public async Task<ServiceResponseDto> UpdateServiceAsync(int id, ServiceUpdateRequestDto serviceUpdateRequestDto, CancellationToken cancellationToken)
        {
            IdValidator.ValidateId(id);
            ArgumentNullException.ThrowIfNull(serviceUpdateRequestDto, nameof(serviceUpdateRequestDto));

            var serviceToUpdate = await unitOfWork.ServiceRepository.GetByExpressionAsync(
                x => x.Id == id && !x.IsDeleted,
                cancellationToken
            );

            if (serviceToUpdate is null)
            {
                throw new NotFoundException($"Service with id {id} does not exist.", nameof(serviceToUpdate));
            }

            serviceToUpdate.IsDeleted = true;

            var newService = new Service
            {
                Name = serviceUpdateRequestDto.Name,
                Description = serviceUpdateRequestDto.Description,
                Duration = serviceUpdateRequestDto.Duration,
                Price = serviceUpdateRequestDto.Price,
                ImageURL = serviceUpdateRequestDto.ImageURL,
                Version = DateTime.UtcNow,
                IsDeleted = false
            };

            await unitOfWork.ServiceRepository.CreateAsync(newService, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return mapper.Map<ServiceResponseDto>(newService);
        }

        public async Task DeleteServiceAsync(int id, CancellationToken cancellationToken)
        {
            IdValidator.ValidateId(id);

            var serviceToDelete = await unitOfWork.ServiceRepository.GetByIdAsync(id, cancellationToken);
            if (serviceToDelete is null)
            {
                throw new NotFoundException($"Service with id {id} does not exist.", nameof(serviceToDelete));
            }

            serviceToDelete.IsDeleted = true;
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}