using Microsoft.AspNetCore.Mvc;
using POS_System.Business.Dtos.Request;
using POS_System.Business.Services.Interfaces;

namespace POS_System.Api.Controllers
{
    [ApiController]
    [Route("/v1/auth")]   
    public class AuthController(IAuthService authService) : ControllerBase
    {
        [HttpPost("register")] // Reiks keist dto pavadinima i register request galimai
        public async Task<IActionResult> RegisterUserAsync([FromBody] UserRequest registerUser)
        {
            var response = await authService.RegisterUserAsync(registerUser);

            return Ok(response);
        }   
        
        [HttpPost("login")]
        public async Task<IActionResult> LoginUserAsync([FromBody] UserLoginRequest credentials)
        {
            var response = await authService.LoginUserAsync(credentials);

            return Ok(response);
        }
    }
}