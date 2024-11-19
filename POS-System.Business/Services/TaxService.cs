using AutoMapper;
using POS_System.Business.Dtos;
using POS_System.Business.Services.Interfaces;
using POS_System.Data.Repositories.Interfaces;
using POS_System.Domain.Entities;

namespace POS_System.Business.Services
{
    public class TaxService(IUnitOfWork _unitOfWork, IMapper _mapper) : ITaxService
    {
        public async Task<IEnumerable<TaxDto?>> GetAllTaxesAsync(CancellationToken cancellationToken)
        {
            var taxes = await _unitOfWork.TaxRepository.GetAllByExpressionAsync(x => x.IsDeleted == false, cancellationToken);
            var taxDtos = _mapper.Map<List<TaxDto>>(taxes);
            return taxDtos;
        }

        public async Task CreateTaxAsync(TaxDto? taxDto, CancellationToken cancellationToken)
        {
            taxDto.TaxId = await _unitOfWork.TaxRepository.GetMaxTaxIdAsync(cancellationToken) + 1;
            var tax = _mapper.Map<Tax>(taxDto);
            await _unitOfWork.TaxRepository.CreateAsync(tax);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteTaxAsync(int id, CancellationToken cancellationToken)
        {
            var tax = await _unitOfWork.TaxRepository.GetByExpressionWithIncludesNoTrackingAsync(x => x.Id == id);

            tax = tax with { IsDeleted = true };

            _unitOfWork.TaxRepository.Update(tax);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateTaxAsync(int id, TaxDto? taxDto, CancellationToken cancellationToken)
        {
            var currentTax = await _unitOfWork.TaxRepository.GetByExpressionWithIncludesNoTrackingAsync(x => x.Id == id && !x.IsDeleted);

            currentTax = currentTax with { IsDeleted = true };
            _unitOfWork.TaxRepository.Update(currentTax);

            var newTax = new Tax
            {
                TaxId = taxDto.TaxId,
                Name = taxDto.Name,
                Rate = taxDto.Rate,
                IsPercentage = taxDto.IsPercentage,
                Version = DateTime.UtcNow,
                IsDeleted = false
            };

            await _unitOfWork.TaxRepository.CreateAsync(newTax);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<TaxDto?> GetTaxByIdAsync(int id, CancellationToken cancellationToken)
        {
            var tax = await _unitOfWork.TaxRepository.GetByIdAsync(id, cancellationToken);
            var taxDto = _mapper.Map<TaxDto>(tax);
            return taxDto;
        }
    }
}
