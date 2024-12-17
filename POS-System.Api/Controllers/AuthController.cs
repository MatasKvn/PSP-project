using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using POS_System.Business.Dtos.Request;
using POS_System.Business.Services;
using POS_System.Business.Services.Interfaces;

namespace POS_System.Api.Controllers
{
    [ApiController]
    [Route("/")]   
    public class AuthController(IAuthService authService) : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost("api/employees/register")]
        public async Task<IActionResult> RegisterUserAsync([FromBody] UserRegisterRequest userRegisterRequest)
        {
            var response = await authService.RegisterUserAsync(userRegisterRequest);

            return Ok(response);
        }   
        
        [AllowAnonymous]
        [HttpPost("v1/auth/login")]
        public async Task<IActionResult> LoginUserAsync([FromBody] UserLoginRequest credentials)
        {
            var response = await authService.LoginUserAsync(credentials);

            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPasswordAsync(ForgotPasswordRequest forgotPasswordRequest)
        {
            var response = await authService.ForgotPasswordAsync(forgotPasswordRequest);

            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPasswordAsync(ResetPasswordRequest resetPasswordRequest)
        {
            var response = await authService.ResetPasswordAsync(resetPasswordRequest);

            return Ok(response);
        }
    }
}