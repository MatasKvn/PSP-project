using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace POS_System.Business.Utils
{
    public class TokenGenerator(IConfiguration configuration) : ITokenGenerator
    {
        public string GenerateJwtToken(List<Claim>? claims = null)
        {
            var secretKey = configuration["POSJwtSecretKey"];
            var issuer = configuration["POSIssuer"];
            var audience = configuration["POSAudience"];

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwtToken = new JwtSecurityToken
            (
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }
    }
}