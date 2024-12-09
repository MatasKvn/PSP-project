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
        public async Task<PagedResponse<ServiceResponse>> GetAllServicesAsync(CancellationToken cancellationToken, int pageNum, int pageSize)
        {
            var (services, totalCount) = await unitOfWork.ServiceRepository.GetAllWithPaginationAsync(
                pageSize,
                pageNum,
            cancellationToken
            );

            var mappedservices = mapper.Map<IEnumerable<ServiceResponse>>(services);
            return new PagedResponse<ServiceResponse>(totalCount, pageSize, pageNum, mappedservices);
        }

        public async Task<ServiceResponse?> GetServiceByIdAsync(int id, CancellationToken cancellationToken)
        {
            IdValidator.ValidateId(id);

            var service = await unitOfWork.ServiceRepository.GetByExpressionAsync(
                x => x.Id == id && !x.IsDeleted,cancellationToken) 
                    ?? throw new NotFoundException($"Service with id {id} does not exist.");

            return mapper.Map<ServiceResponse>(service);
        }

        public async Task<ServiceResponse> CreateServiceAsync(ServiceRequest serviceRequest, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(serviceRequest, nameof(serviceRequest));

            var newService = mapper.Map<Service>(serviceRequest);

            newService.Version = DateTime.UtcNow;
            newService.IsDeleted = false;

            await unitOfWork.ServiceRepository.CreateAsync(newService, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return mapper.Map<ServiceResponse>(newService);
        }

        public async Task<ServiceResponse> UpdateServiceAsync(int id, ServiceRequest serviceRequest, CancellationToken cancellationToken)
        {
            IdValidator.ValidateId(id);
            ArgumentNullException.ThrowIfNull(serviceRequest, nameof(serviceRequest));

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
                Name = serviceRequest.Name,
                Description = serviceRequest.Description,
                Duration = serviceRequest.Duration,
                Price = serviceRequest.Price,
                ImageURL = serviceRequest.ImageURL,
                Version = DateTime.UtcNow,
                IsDeleted = false
            };

            await unitOfWork.ServiceRepository.CreateAsync(newService, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return mapper.Map<ServiceResponse>(newService);
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