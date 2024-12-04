using POS_System.Business.Dtos;
using POS_System.Business.Dtos.Request;
using POS_System.Business.Dtos.Response;

namespace POS_System.Business.Services.Interfaces
{
    public interface IServiceOfService
    {
        Task<ServiceResponse> CreateServiceAsync(ServiceRequest serviceRequest, CancellationToken cancellationToken);
        Task<PagedResponse<ServiceResponse>> GetAllServicesAsync(CancellationToken cancellationToken, int pageNum, int pageSize);
        Task<ServiceResponse?> GetServiceByIdAsync(int id, CancellationToken cancellationToken);
        Task<ServiceResponse> UpdateServiceAsync(int id, ServiceRequest serviceRequest, CancellationToken cancellationToken);
        Task DeleteServiceAsync(int id, CancellationToken cancellationToken);
    }
}
