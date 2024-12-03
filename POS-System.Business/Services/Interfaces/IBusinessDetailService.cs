using POS_System.Business.Dtos.BusinessDetails;

namespace POS_System.Business.Services.Interfaces
{
    public interface IBusinessDetailService
    {
        public Task<BusinessDetailsResponseDto> GetBusinessDetailsAsync(CancellationToken cancellationToken);
        public Task<BusinessDetailsResponseDto> CreateOrUpdateBusinessDetailsAsync(BusinessDetailsRequestDto businessDetailsRequestDto, CancellationToken cancellationToken);
    }
}
