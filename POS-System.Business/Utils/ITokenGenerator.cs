using System.Security.Claims;

namespace POS_System.Business.Utils
{
    public interface ITokenGenerator
    {
        public string GenerateJwtToken(List<Claim> claims);
    }
}