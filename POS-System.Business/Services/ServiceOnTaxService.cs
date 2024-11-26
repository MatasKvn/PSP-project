using AutoMapper;
using Microsoft.CodeAnalysis;
using POS_System.Business.Services.Interfaces;
using POS_System.Data.Repositories.Interfaces;
using POS_System.Domain.Entities;

namespace POS_System.Business.Services
{
    public class ServiceOnTaxService(IUnitOfWork _unitOfWork, IMapper _mapper) : IServiceOnTaxService
    {
        public async Task MarkActiveTaxLinksDeleted(int taxId, CancellationToken cancellationToken)
        {
            var serviceLinks = await _unitOfWork.ServiceOnTaxRepository.GetAllByExpressionAsync(x => x.TaxVersionId == taxId && x.EndDate == null, cancellationToken);

            foreach (var serviceLink in serviceLinks)
            {
                serviceLink.EndDate = DateTime.UtcNow;
            }

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task RelinkTaxToItem(int oldTaxId, int newTaxId, CancellationToken cancellationToken)
        {
            var serviceLinks = await _unitOfWork.ServiceOnTaxRepository.GetAllByExpressionAsync(x => x.TaxVersionId == oldTaxId && x.EndDate == null, cancellationToken);

            foreach (var serviceLink in serviceLinks)
            {
                serviceLink.EndDate = DateTime.UtcNow;

                var newServiceOnTax = new ServiceOnTax
                {
                    TaxVersionId = newTaxId,
                    ServiceVersionId = serviceLink.ServiceVersionId,
                    StartDate = DateTime.UtcNow,
                    EndDate = null
                };

                await _unitOfWork.ServiceOnTaxRepository.CreateAsync(newServiceOnTax);
            }

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task LinkTaxToServicesAsync(int taxId, int[] serviceIdList, CancellationToken cancellationToken)
        {
            var tax = await _unitOfWork.TaxRepository.GetByIdAsync(taxId, cancellationToken);
            if (tax is not null && tax.IsDeleted == false)
            {
                foreach (var serviceId in serviceIdList)
                {
                    var service = await _unitOfWork.ServiceRepository.GetByIdAsync(serviceId, cancellationToken);
                    if (service is not null && service.IsDeleted == false)
                    {
                        var existingLink = await _unitOfWork.ServiceOnTaxRepository.GetByExpressionWithIncludesAsync(x => x.TaxVersionId == taxId && x.ServiceVersionId == serviceId);
                        if (existingLink is null)
                        {
                            var link = new ServiceOnTax
                            {
                                TaxVersionId = taxId,
                                ServiceVersionId = serviceId,
                                StartDate = DateTime.UtcNow,
                                EndDate = null
                            };

                            await _unitOfWork.ServiceOnTaxRepository.CreateAsync(link, cancellationToken);
                        }
                    }
                }

                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task UnlinkTaxFromServicesAsync(int taxId, int[] serviceIdList, CancellationToken cancellationToken)
        {
            var tax = await _unitOfWork.TaxRepository.GetByIdAsync(taxId, cancellationToken);
            if (tax is not null && tax.IsDeleted == false)
            {
                foreach (var serviceId in serviceIdList)
                {
                    var service = await _unitOfWork.ServiceRepository.GetByIdAsync(serviceId, cancellationToken);
                    if (service is not null && service.IsDeleted == false)
                    {
                        var existingLink = await _unitOfWork.ServiceOnTaxRepository.GetByExpressionWithIncludesAsync(x => x.TaxVersionId == taxId && x.ServiceVersionId == serviceId);
                        if (existingLink is not null)
                        {
                            existingLink.EndDate = DateTime.UtcNow;
                        }
                    }
                }
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
