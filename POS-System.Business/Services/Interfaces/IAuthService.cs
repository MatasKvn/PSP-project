using Microsoft.AspNetCore.Identity;
using POS_System.Business.Dtos.Request;
using POS_System.Business.Dtos.Response;

namespace POS_System.Business.Services.Interfaces
{
    public interface IAuthService
    {
        Task<IdentityResult> RegisterUserAsync(UserRequest registerUser);
        Task<UserLoginResponse> LoginUserAsync(UserLoginRequest credentials);
    }
}