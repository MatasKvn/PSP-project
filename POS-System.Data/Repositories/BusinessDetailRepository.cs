using Microsoft.Extensions.Configuration;
using POS_System.Data.Repositories.Interfaces;
using POS_System.Domain.Entities;
using System.Text.Json;

namespace POS_System.Data.Repositories
{
    public class BusinessDetailRepository : IBusinessDetailRepository
    {
        private readonly string FileName;
        private readonly string RelativePath;
        private readonly string FullPath;
        
        public BusinessDetailRepository(IConfiguration _configuration)
        {
            FileName = _configuration["BusinessDetails:FileName"]
                ?? throw new ArgumentNullException(nameof(FileName));

            RelativePath = _configuration["BusinessDetails:RelativePath"]
                ?? throw new ArgumentNullException(nameof(RelativePath));

            FullPath = RelativePath + FileName;
        }

        public async Task<BusinessDetails> FetchBusinessDetailsAsync(CancellationToken cancellationToken)
        {
            if (!File.Exists(FullPath))
            {
                throw new FileNotFoundException($"The business details file at path {FullPath} does not exist.");
            }

            var fileContent = await File.ReadAllTextAsync(FullPath, cancellationToken);
            var businessDetails = JsonSerializer.Deserialize<BusinessDetails>(fileContent);

            if (businessDetails is null)
            {
                throw new JsonException("Failed to deserialize business details");
            }

            return businessDetails;
        }

        public async Task CreateOrReplaceBusinessDetailsAsync(BusinessDetails businessDetails, CancellationToken cancellationToken)
        {
            var serializedBusinessDetails = JsonSerializer.Serialize(businessDetails);
            await File.WriteAllTextAsync(FullPath, serializedBusinessDetails, cancellationToken);
        }
    }
}
