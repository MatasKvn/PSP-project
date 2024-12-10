using POS_System.Business.Dtos;
using POS_System.Business.Dtos.Request;
using POS_System.Business.Dtos.Response;

namespace POS_System.Business.Services.Interfaces
{
    public interface IServiceOfService
    {
        public Task<ServiceResponse> CreateServiceAsync(ServiceRequest serviceRequest, CancellationToken cancellationToken);
        public Task<PagedResponse<ServiceResponse>> GetAllServicesAsync(CancellationToken cancellationToken, int pageNum, int pageSize);
        public Task<ServiceResponse?> GetServiceByIdAsync(int id, CancellationToken cancellationToken);
        public Task<ServiceResponse> UpdateServiceAsync(int id, ServiceRequest serviceRequest, CancellationToken cancellationToken);
        public Task DeleteServiceAsync(int id, CancellationToken cancellationToken);
        public Task LinkServiceToTaxesAsync(int serviceId, int[] taxIdList, CancellationToken cancellationToken);
        public Task UnlinkServiceFromTaxesAsync(int serviceId, int[] taxIdList, CancellationToken cancellationToken);
        public Task<IEnumerable<ServiceResponse>> GetServicesLinkedToTaxId(int taxId, DateTime? timeStamp, CancellationToken cancellationToken);
    }
}
