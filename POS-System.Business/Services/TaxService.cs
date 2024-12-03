using AutoMapper;
using POS_System.Business.Dtos.Tax;
using POS_System.Business.Services.Interfaces;
using POS_System.Common.Constants;
using POS_System.Common.Exceptions;
using POS_System.Data.Repositories;
using POS_System.Data.Repositories.Interfaces;
using POS_System.Domain.Entities;

namespace POS_System.Business.Services
{
    public class TaxService(IUnitOfWork _unitOfWork, IManyToManyService<Product, Tax, ProductOnTax> _productOnTaxService, IManyToManyService<Service, Tax, ServiceOnTax> _serviceOnTaxService,  IMapper _mapper) : ITaxService
    {
        public async Task<IEnumerable<TaxResponseDto>> GetAllTaxesAsync(int pageSize, int pageNumber, CancellationToken cancellationToken)
        {
            var (taxes, totalCount) = await _unitOfWork.TaxRepository.GetByExpressionWithIncludesAndPaginationAsync(
                x => x.IsDeleted != true,
                pageSize,
                pageNumber,
                cancellationToken
            );

            var taxDtos = _mapper.Map<List<TaxResponseDto>>(taxes);
            return taxDtos;
        }

        public async Task<TaxResponseDto> CreateTaxAsync(TaxRequestDto taxDto, CancellationToken cancellationToken)
        {
            var tax = _mapper.Map<Tax>(taxDto);
            tax.IsDeleted = false;
            tax.Version = DateTime.UtcNow;

            await _unitOfWork.TaxRepository.CreateAsync(tax);
            await _unitOfWork.SaveChangesAsync();

            var responseTaxDto = _mapper.Map<TaxResponseDto>(tax);
            return responseTaxDto;
        }

        public async Task DeleteTaxAsync(int id, CancellationToken cancellationToken)
        {
            var tax = await _unitOfWork.TaxRepository.GetByIdAsync(id, cancellationToken)
                ?? throw new NotFoundException(ApplicationMesssages.TAX_NOT_FOUND);

            tax.IsDeleted = true;
            await _productOnTaxService.MarkActiveLinksDeletedAsync(_unitOfWork.ProductOnTaxRepository, id, false, cancellationToken);
            await _serviceOnTaxService.MarkActiveLinksDeletedAsync(_unitOfWork.ServiceOnTaxRepository, id, false, cancellationToken);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<TaxResponseDto> UpdateTaxAsync(int id, TaxRequestDto taxDto, CancellationToken cancellationToken)
        {
            var currentTax = await _unitOfWork.TaxRepository.GetByIdAsync(id, cancellationToken)
                ?? throw new NotFoundException(ApplicationMesssages.TAX_NOT_FOUND);

            currentTax.IsDeleted = true;

            var newTax = _mapper.Map<Tax>(taxDto);
            newTax.TaxId = currentTax.TaxId;
            newTax.IsDeleted = false;
            newTax.Version = DateTime.UtcNow;

            await _unitOfWork.TaxRepository.CreateAsync(newTax, cancellationToken);
            await _unitOfWork.SaveChangesAsync();

            var newTaxDto = _mapper.Map<TaxResponseDto>(newTax);

            await _productOnTaxService.RelinkItemToItem(_unitOfWork.ProductOnTaxRepository, id, newTax.Id, false, cancellationToken);
            await _serviceOnTaxService.RelinkItemToItem(_unitOfWork.ServiceOnTaxRepository, id, newTax.Id, false, cancellationToken);

            return newTaxDto;
        }

        public async Task<TaxResponseDto> GetTaxByIdAsync(int id, CancellationToken cancellationToken)
        {
            var tax = await _unitOfWork.TaxRepository.GetByIdAsync(id, cancellationToken)
                ?? throw new NotFoundException(ApplicationMesssages.TAX_NOT_FOUND);

            var taxDto = _mapper.Map<TaxResponseDto>(tax);
            return taxDto;
        }

        public async Task LinkTaxToItemsAsync(int taxId, bool itemsAreProducts, int[] itemIdList, CancellationToken cancellationToken)
        {
            if (itemsAreProducts)
            {
                await _productOnTaxService.LinkItemToItemsAsync(_unitOfWork.ProductRepository, _unitOfWork.TaxRepository, _unitOfWork.ProductOnTaxRepository, taxId, itemIdList, false, cancellationToken);
            }
            else
            {
                await _serviceOnTaxService.LinkItemToItemsAsync(_unitOfWork.ServiceRepository, _unitOfWork.TaxRepository, _unitOfWork.ServiceOnTaxRepository, taxId, itemIdList, false, cancellationToken);
            }
            
        }

        public async Task UnlinkTaxFromItemsAsync(int taxId, bool itemsAreProducts, int[] itemIdList, CancellationToken cancellationToken)
        {
            if (itemsAreProducts)
            {
                await _productOnTaxService.UnlinkItemFromItemsAsync(_unitOfWork.ProductRepository, _unitOfWork.TaxRepository, _unitOfWork.ProductOnTaxRepository, taxId, itemIdList, false, cancellationToken);
            }
            else
            {
                await _serviceOnTaxService.UnlinkItemFromItemsAsync(_unitOfWork.ServiceRepository, _unitOfWork.TaxRepository, _unitOfWork.ServiceOnTaxRepository, taxId, itemIdList, false, cancellationToken);
            }
        }
            

        public async Task<IEnumerable<TaxResponseDto>> GetTaxesLinkedToItemId(int itemId, bool isProduct, DateTime? timeStamp, CancellationToken cancellationToken)
        {
            IEnumerable<int> taxLinkIds;
            IList<Tax> taxes = new List<Tax>();

            if (timeStamp is null)
            {
                taxLinkIds = isProduct ?
                    await _productOnTaxService.GetLinkIdsAsync(_unitOfWork.ProductOnTaxRepository, itemId, true, null, cancellationToken)
                    : await _serviceOnTaxService.GetLinkIdsAsync(_unitOfWork.ServiceOnTaxRepository, itemId, true, null, cancellationToken);
            }
            else
            {
                taxLinkIds = isProduct ?
                    await _productOnTaxService.GetLinkIdsAsync(_unitOfWork.ProductOnTaxRepository, itemId, true, timeStamp, cancellationToken)
                    : await _serviceOnTaxService.GetLinkIdsAsync(_unitOfWork.ServiceOnTaxRepository, itemId, true, timeStamp, cancellationToken);
            }

            foreach (var taxId in taxLinkIds)
            {
                var tax = await _unitOfWork.TaxRepository.GetByIdAsync(taxId, cancellationToken);

                if (tax is not null)
                    taxes.Add(tax);
            }

            var taxDtos = _mapper.Map<List<TaxResponseDto>>(taxes);
            return taxDtos;
        }
    }
}
