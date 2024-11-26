using AutoMapper;
using POS_System.Business.Dtos.Tax;
using POS_System.Business.Services.Interfaces;
using POS_System.Data.Repositories.Interfaces;
using POS_System.Domain.Entities;

namespace POS_System.Business.Services
{
    public class TaxService(IUnitOfWork _unitOfWork, IProductOnTaxService _productOnTaxService, IServiceOnTaxService _serviceOnTaxService, IMapper _mapper) : ITaxService
    {
        public async Task<IEnumerable<TaxResponseDto>> GetAllTaxesAsync(CancellationToken cancellationToken)
        {
            var taxes = await _unitOfWork.TaxRepository.GetAllByExpressionAsync(x => x.IsDeleted == false, cancellationToken);
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
                ?? throw new Exception("No such tax to delete!");

            tax.IsDeleted = true;
            await _productOnTaxService.MarkActiveTaxLinksDeleted(id, cancellationToken);
            await _serviceOnTaxService.MarkActiveTaxLinksDeleted(id, cancellationToken);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<TaxResponseDto> UpdateTaxAsync(int id, TaxRequestDto taxDto, CancellationToken cancellationToken)
        {
            var currentTax = await _unitOfWork.TaxRepository.GetByIdAsync(id, cancellationToken)
                ?? throw new Exception("No such tax to update!");

            currentTax.IsDeleted = true;

            var newTax = _mapper.Map<Tax>(taxDto);
            newTax.TaxId = currentTax.TaxId;
            newTax.IsDeleted = false;
            newTax.Version = DateTime.UtcNow;

            await _unitOfWork.TaxRepository.CreateAsync(newTax, cancellationToken);
            await _unitOfWork.SaveChangesAsync();

            await _productOnTaxService.RelinkTaxToItem(id, newTax.Id, cancellationToken);
            await _serviceOnTaxService.RelinkTaxToItem(id, newTax.Id, cancellationToken);

            var newTaxDto = _mapper.Map<TaxResponseDto>(newTax);
            
            return newTaxDto;
        }

        public async Task<TaxResponseDto> GetTaxByIdAsync(int id, CancellationToken cancellationToken)
        {
            var tax = await _unitOfWork.TaxRepository.GetByIdAsync(id, cancellationToken)
                ?? throw new Exception("No such tax exists!");

            var taxDto = _mapper.Map<TaxResponseDto>(tax);
            return taxDto;
        }
    }
}
