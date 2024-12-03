using POS_System.Business.Dtos.Request;
using POS_System.Business.Dtos.Response;

namespace POS_System.Business.Services.Interfaces
{
    public interface IBusinessDetailService
    {
        public Task<BusinessDetailsResponse> GetBusinessDetailsAsync(CancellationToken cancellationToken);
        public Task<BusinessDetailsResponse> CreateOrUpdateBusinessDetailsAsync(BusinessDetailsRequest businessDetailsRequestDto, CancellationToken cancellationToken);
    }
}
