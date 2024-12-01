using AutoMapper;
using POS_System.Business.Dtos.BusinessDetails;
using POS_System.Business.Services.Interfaces;
using POS_System.Data.Repositories.Interfaces;
using POS_System.Domain.Entities;

namespace POS_System.Business.Services
{
    public class BusinessDetailService(IUnitOfWork _unitOfWork, IMapper _mapper) : IBusinessDetailService
    {
        public async Task<BusinessDetailsResponseDto> GetBusinessDetailsAsync(CancellationToken cancellationToken)
        {
            var businessDetails = await _unitOfWork.BusinessDetailRepository.FetchBusinessDetailsAsync(cancellationToken);
            var businessDetailsDto = _mapper.Map<BusinessDetailsResponseDto>(businessDetails);

            return businessDetailsDto;
        }

        public async Task<BusinessDetailsResponseDto> CreateOrUpdateBusinessDetailsAsync(BusinessDetailsRequestDto businessDetailsRequestDto, CancellationToken cancellationToken)
        {
            var businessDetails = _mapper.Map<BusinessDetails>(businessDetailsRequestDto);
            await _unitOfWork.BusinessDetailRepository.CreateOrReplaceBusinessDetailsAsync(businessDetails, cancellationToken);

            var businessDetailsResponseDto = _mapper.Map<BusinessDetailsResponseDto>(businessDetails);
            return businessDetailsResponseDto;
        }
    }
}
