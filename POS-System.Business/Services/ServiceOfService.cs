using AutoMapper;
using POS_System.Business.Validators;
using POS_System.Business.Services.Interfaces;
using POS_System.Data.Repositories.Interfaces;
using POS_System.Domain.Entities;
using POS_System.Common.Exceptions;
using POS_System.Business.Dtos;
using POS_System.Business.Dtos.Response;
using POS_System.Business.Dtos.Request;
using POS_System.Data.Repositories;
using POS_System.Common.Constants;

namespace POS_System.Business.Services
{
    public class ServiceOfService(IUnitOfWork _unitOfWork, IManyToManyService<Service, Tax, ServiceOnTax> _serviceOnTaxService, IManyToManyService<Service, ItemDiscount, ServiceOnItemDiscount> _serviceOnItemDiscountService, IMapper _mapper) : IServiceOfService
    {
        public async Task<PagedResponse<ServiceResponse>> GetAllServicesAsync(CancellationToken cancellationToken, int pageNum, int pageSize)
        {
            var (services, totalCount) = await _unitOfWork.ServiceRepository.GetByExpressionWithIncludesAndPaginationAsync(
                x => x.IsDeleted == false,
                pageSize,
                pageNum,
            cancellationToken
            );

            var mappedservices = _mapper.Map<IEnumerable<ServiceResponse>>(services);
            return new PagedResponse<ServiceResponse>(totalCount, pageSize, pageNum, mappedservices);
        }

        public async Task<ServiceResponse?> GetServiceByIdAsync(int id, CancellationToken cancellationToken)
        {
            IdValidator.ValidateId(id);

            var service = await _unitOfWork.ServiceRepository.GetByExpressionAsync(
                x => x.Id == id && !x.IsDeleted,cancellationToken) 
                    ?? throw new NotFoundException($"Service with id {id} does not exist.");

            return _mapper.Map<ServiceResponse>(service);
        }

        public async Task<ServiceResponse> CreateServiceAsync(ServiceRequest serviceRequest, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(serviceRequest, nameof(serviceRequest));

            var newService = _mapper.Map<Service>(serviceRequest);

            newService.Version = DateTime.UtcNow;
            newService.IsDeleted = false;

            await _unitOfWork.ServiceRepository.CreateAsync(newService, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<ServiceResponse>(newService);
        }

        public async Task<ServiceResponse> UpdateServiceAsync(int id, ServiceRequest serviceRequest, CancellationToken cancellationToken)
        {
            IdValidator.ValidateId(id);
            ArgumentNullException.ThrowIfNull(serviceRequest, nameof(serviceRequest));

            var serviceToUpdate = await _unitOfWork.ServiceRepository.GetByExpressionAsync(
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
                IsDeleted = false,
                EmployeeId = serviceRequest.EmployeeId,
            };

            await _unitOfWork.ServiceRepository.CreateAsync(newService, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            await _serviceOnTaxService.RelinkItemToItemAsync(_unitOfWork.ServiceOnTaxRepository, id, newService.Id, true, cancellationToken);
            await _serviceOnItemDiscountService.RelinkItemToItemAsync(_unitOfWork.ServiceOnItemDiscountRepository, id, newService.Id, true, cancellationToken);

            return _mapper.Map<ServiceResponse>(newService);
        }

        public async Task DeleteServiceAsync(int id, CancellationToken cancellationToken)
        {
            IdValidator.ValidateId(id);

            var serviceToDelete = await _unitOfWork.ServiceRepository.GetByIdAsync(id, cancellationToken);
            if (serviceToDelete is null)
            {
                throw new NotFoundException($"Service with id {id} does not exist.", nameof(serviceToDelete));
            }

            serviceToDelete.IsDeleted = true;

            await _serviceOnTaxService.MarkActiveLinksDeletedAsync(_unitOfWork.ServiceOnTaxRepository, id, true, cancellationToken);
            await _serviceOnItemDiscountService.MarkActiveLinksDeletedAsync(_unitOfWork.ServiceOnItemDiscountRepository, id, true, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<ServiceResponse>> GetServicesLinkedToTaxId(int taxId, DateTime? timeStamp, CancellationToken cancellationToken)
        {
            IEnumerable<int> serviceLinkIds;
            IList<Service> services = new List<Service>();

            serviceLinkIds = await _serviceOnTaxService.GetLinkIdsAsync(_unitOfWork.ServiceOnTaxRepository, taxId, false, timeStamp, cancellationToken);

            foreach (var serviceId in serviceLinkIds)
            {
                var service = await _unitOfWork.ServiceRepository.GetByIdAsync(serviceId, cancellationToken);

                if (service is not null)
                    services.Add(service);
            }

            var serviceDtos = _mapper.Map<List<ServiceResponse>>(services);
            return serviceDtos;
        }

        public async Task<IEnumerable<ServiceResponse>> GetServicesLinkedToItemDiscountId(int itemDiscountId, DateTime? timeStamp, CancellationToken cancellationToken)
        {
            IEnumerable<int> serviceLinkIds;
            IList<Service> services = new List<Service>();

            var itemDiscount = await _unitOfWork.ItemDiscountRepository.GetByIdAsync(itemDiscountId, cancellationToken)
                ?? throw new NotFoundException(ApplicationMessages.NOT_FOUND_ERROR);

            serviceLinkIds = await _serviceOnItemDiscountService.GetLinkIdsAsync(_unitOfWork.ServiceOnItemDiscountRepository, itemDiscountId, false, timeStamp, cancellationToken);

            foreach (var serviceId in serviceLinkIds)
            {
                var service = await _unitOfWork.ServiceRepository.GetByIdAsync(serviceId, cancellationToken);

                if (service is not null)
                {
                    if ((itemDiscount.StartDate is null && itemDiscount.EndDate is null) || (itemDiscount.StartDate <= timeStamp && itemDiscount.EndDate >= timeStamp))
                        services.Add(service);
                }
            }

            var serviceDtos = _mapper.Map<List<ServiceResponse>>(services);
            return serviceDtos;
        }
    }
}