using POS_System.Business.Dtos;
using POS_System.Business.Dtos.Request;
using POS_System.Business.Dtos.Response;

namespace POS_System.Business.Services.Interfaces
{
    public interface IServiceOfService
    {
        Task<ServiceResponseDto> CreateServiceAsync(ServiceRequestDto serviceRequestDto, CancellationToken cancellationToken);
        Task<PagedResponse<ServiceResponseDto>> GetAllServicesAsync(CancellationToken cancellationToken, int pageNum, int pageSize);
        Task<ServiceResponseDto?> GetServiceByIdAsync(int id, CancellationToken cancellationToken);
        Task<ServiceResponseDto> UpdateServiceAsync(int id, ServiceUpdateRequestDto serviceUpdateRequestDto, CancellationToken cancellationToken);
        Task DeleteServiceAsync(int id, CancellationToken cancellationToken);
    }
}
