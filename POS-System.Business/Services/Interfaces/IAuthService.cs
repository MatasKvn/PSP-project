using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using POS_System.Business.Dtos.Request;
using POS_System.Business.Dtos.Response;

namespace POS_System.Business.Services.Interfaces
{
    public interface IAuthService
    {
        Task<IdentityResult> RegisterUserAsync(UserRegisterRequest registerUser);
        Task<UserLoginResponse> LoginUserAsync(UserLoginRequest credentials);
        Task<PasswordRecoveryResponse> ForgotPasswordAsync(ForgotPasswordRequest forgotPasswordRequest);
        Task<PasswordRecoveryResponse> ResetPasswordAsync(ResetPasswordRequest resetPasswordRequest);
    }
}