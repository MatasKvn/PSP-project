using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using POS_System.Business.Dtos.Request;
using POS_System.Business.Dtos.Response;
using POS_System.Business.Services.Interfaces;
using POS_System.Business.Utils;
using POS_System.Common.Constants;
using POS_System.Common.Exceptions;
using ApplicationUser = POS_System.Data.Identity.ApplicationUser<int>;

namespace POS_System.Business.Services
{
    public class AuthService(
        UserManager<ApplicationUser> userManager, 
        SignInManager<ApplicationUser> signInManager, 
        IMapper mapper,
        ITokenGenerator tokenGenerator
    ) : IAuthService
    {
        public async Task<IdentityResult> RegisterUserAsync(UserRequest registerUser)
        {
            var applicationUser = mapper.Map<ApplicationUser>(registerUser);
            
            applicationUser.StartDate = DateOnly.FromDateTime(DateTime.Now);
            applicationUser.Version = DateTime.Now;

            var response = await userManager.CreateAsync(applicationUser) 
                ?? throw new UserCreationException(StatusCodes.Status409Conflict, ErrorMessages.USER_CREATION_FAIL);
            
            return response;
        }

        public async Task<UserLoginResponse> LoginUserAsync(UserLoginRequest credentials)
        {
            var result = await signInManager.PasswordSignInAsync(credentials.UserName, credentials.Password, false, false);

            if (result.IsLockedOut)
                throw new LockedOutException(StatusCodes.Status429TooManyRequests, ErrorMessages.ACCOUNT_LOCKED_OUT); 

            if (!result.Succeeded)
                throw new InvalidSignInCredsException(StatusCodes.Status401Unauthorized, ErrorMessages.INVALID_SIGN_IN_CREDS);
            
            var jwtToken = tokenGenerator.GenerateJwtToken();

            return new UserLoginResponse(credentials.UserName, jwtToken);
        }
    }
}