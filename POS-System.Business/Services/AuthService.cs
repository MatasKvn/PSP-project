using AutoMapper;
using Microsoft.AspNetCore.Identity;
using POS_System.Business.Dtos.Request;
using POS_System.Business.Dtos.Response;
using POS_System.Business.Services.Interfaces;
using POS_System.Business.Utils;
using POS_System.Common.Constants;
using POS_System.Common.Exceptions;
using POS_System.Data.Identity;

namespace POS_System.Business.Services
{
    public class AuthService(
        UserManager<ApplicationUser> userManager, 
        SignInManager<ApplicationUser> signInManager,
        RoleManager<ApplicationRole> roleManager,
        IMapper mapper,
        ITokenGenerator tokenGenerator
    ) : IAuthService
    {
        public async Task<IdentityResult> RegisterUserAsync(UserRequest registerUser)
        {
            var user = mapper.Map<ApplicationUser>(registerUser);
            
            user.StartDate = DateOnly.FromDateTime(DateTime.Now);
            user.Version = DateTime.Now;

            var response = await userManager.CreateAsync(user, registerUser.Password);

            if (!response.Succeeded)
                return response;

            var createdUser = await userManager.FindByNameAsync(registerUser.UserName);
            await userManager.AddToRoleAsync(createdUser!, "None");
            
            return response;
        }

        public async Task<UserLoginResponse> LoginUserAsync(UserLoginRequest credentials)
        {
            var user = await userManager.FindByNameAsync(credentials.UserName);

            if (user is null || user!.IsDeleted || user.EndDate.HasValue)
                throw new UnauthorizedException(ErrorMessages.INVALID_SIGN_IN_CREDS);

            var result = await signInManager.CheckPasswordSignInAsync(user, credentials.Password, false);

            if (result.IsLockedOut)
                throw new TooManyRequestsException(ErrorMessages.ACCOUNT_LOCKED_OUT);

            if (!result.Succeeded)
                throw new UnauthorizedException(ErrorMessages.INVALID_SIGN_IN_CREDS);

            var roleName = (await userManager.GetRolesAsync(user)).First();
            var role = await roleManager.FindByNameAsync(roleName);
            var claims = (await roleManager.GetClaimsAsync(role!)).Where(c => c.Value == "Y").ToList();
            var jwtToken = tokenGenerator.GenerateJwtToken(claims);

            return new UserLoginResponse(user.Id, credentials.UserName, roleName,jwtToken);
        }
    }
}