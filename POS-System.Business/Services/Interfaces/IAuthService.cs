using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using POS_System.Business.Dtos.Request;
using POS_System.Business.Dtos.Response;
using POS_System.Data.Identity;

namespace POS_System.Business.Services.Interfaces
{
    public interface IAuthService
    {
        Task<EmployeeResponse> RegisterUserAsync(UserRegisterRequest registerUser);
        Task<UserLoginResponse> LoginUserAsync(UserLoginRequest credentials);
        Task<PasswordRecoveryResponse> ForgotPasswordAsync(ForgotPasswordRequest forgotPasswordRequest);
        Task<PasswordRecoveryResponse> ResetPasswordAsync(ResetPasswordRequest resetPasswordRequest);
    }
}