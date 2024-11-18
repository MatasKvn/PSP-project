using AutoMapper;
using POS_System.Business.Dtos;
using POS_System.Business.Services.Interfaces;
using POS_System.Data.Repositories.Interfaces;

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
    }
}
