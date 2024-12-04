using POS_System.Domain.Entities;

namespace POS_System.Data.Repositories.Interfaces
{
    public interface IBusinessDetailRepository
    {
        public Task<BusinessDetails> FetchBusinessDetailsAsync(CancellationToken cancellationToken);
        public Task CreateOrReplaceBusinessDetailsAsync(BusinessDetails businessDetails, CancellationToken cancellationToken);
    }
}
