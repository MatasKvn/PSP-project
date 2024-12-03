using POS_System.Business.Dtos.Request;
using POS_System.Business.Dtos.Response;
using POS_System.Business.Dtos;

namespace POS_System.Business.Services.Interfaces
{
    public interface IServiceReservationService
    {
        public Task<PagedResponse<ServiceReservationResponse?>> GetServiceReservationsAsync(int pageSize, int pageNumber, CancellationToken cancellationToken);
        public Task<ServiceReservationResponse?> GetServiceReservationByIdAsync(int id, CancellationToken cancellationToken);
        public Task<ServiceReservationResponse> CreateServiceReservationAsync(ServiceReservationRequest? serviceReservationDto, CancellationToken cancellationToken);
        public Task<ServiceReservationResponse> UpdateServiceReservationByIdAsync(int id, ServiceReservationRequest? serviceReservationDto, CancellationToken cancellationToken);
    }
}
