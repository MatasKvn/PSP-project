using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Newtonsoft.Json;
using POS_System.Business.Dtos.Request;
using POS_System.Business.Dtos.Response;
using POS_System.Business.Services.Interfaces;
using POS_System.Business.Utils;
using POS_System.Common.Constants;
using POS_System.Common.Enums;
using POS_System.Common.Exceptions;
using POS_System.Data.Identity;

namespace POS_System.Business.Services
{
    public class AuthService(
        UserManager<ApplicationUser> userManager, 
        SignInManager<ApplicationUser> signInManager,
        RoleManager<ApplicationRole> roleManager,
        IMapper mapper,
        ITokenGenerator tokenGenerator,
        IEmailSender emailSender
    ) : IAuthService
    {
        public async Task<IdentityResult> RegisterUserAsync(UserRegisterRequest registerUser)
        {
            var user = mapper.Map<ApplicationUser>(registerUser);

            user.StartDate = DateOnly.FromDateTime(DateTime.UtcNow);
            user.Version = DateTime.UtcNow;

            var response = await userManager.CreateAsync(user, registerUser.Password);

            if (!response.Succeeded)
                throw new BadRequestException(JsonConvert.SerializeObject(response.Errors));

            string newRole = GetRoleById(registerUser.RoleId);

            await userManager.AddToRoleAsync(user!, newRole);

            return response;
        }

        public async Task<UserLoginResponse> LoginUserAsync(UserLoginRequest credentials)
        {
            var user = await userManager.FindByNameAsync(credentials.UserName);

            if (user is null || user!.IsDeleted || user.EndDate.HasValue)
                throw new UnauthorizedException(ApplicationMessages.INVALID_SIGN_IN_CREDS);

            var result = await signInManager.CheckPasswordSignInAsync(user, credentials.Password, false);

            if (result.IsLockedOut)
                throw new TooManyRequestsException(ApplicationMessages.ACCOUNT_LOCKED_OUT);

            if (!result.Succeeded)
                throw new UnauthorizedException(ApplicationMessages.INVALID_SIGN_IN_CREDS);

            var roleName = (await userManager.GetRolesAsync(user)).First();
            var role = await roleManager.FindByNameAsync(roleName);
            var claims = (await roleManager.GetClaimsAsync(role!)).Where(c => c.Value == "Y").ToList();
            var jwtToken = tokenGenerator.GenerateJwtToken(claims);

            return new UserLoginResponse(user.Id, credentials.UserName, roleName,jwtToken);
        }

        public async Task<PasswordRecoveryResponse> ForgotPasswordAsync(ForgotPasswordRequest forgotPasswordRequest)
        {
            var user = await userManager.FindByEmailAsync(forgotPasswordRequest.Email);

            if (user is not null)
            {
                var token = await userManager.GeneratePasswordResetTokenAsync(user);
                var message = new Message(forgotPasswordRequest.Email, ApplicationMessages.PASSWORD_RESET_MAIL_TITLE, $"Your reset token: {token}");
                await emailSender.SendAsync(message);
            }

            return new PasswordRecoveryResponse(ApplicationMessages.EMAIL_SENT_INFO);
        }

        public async Task<PasswordRecoveryResponse> ResetPasswordAsync(ResetPasswordRequest resetPasswordRequest)
        {
            var user = await userManager.FindByEmailAsync(resetPasswordRequest.Email)
                ?? throw new BadRequestException(ApplicationMessages.INVALID_PASS_RECOVERY_CREDS);

            var response = await userManager.ResetPasswordAsync(user, resetPasswordRequest.ResetCode, resetPasswordRequest.NewPassword);

            if (!response.Succeeded)
                throw new BadRequestException(JsonConvert.SerializeObject(response.Errors));

            return new PasswordRecoveryResponse(ApplicationMessages.SUCCESSFUL_ACTION);
        }

        private static string GetRoleById(int roleId)
        {
            return roleId switch
            {
                (int)AccesibilityEnum.SUPER_ADMIN => "Super admin",
                (int)AccesibilityEnum.SERVICE_PROVIDER => "Service provider",
                (int)AccesibilityEnum.CASHIER => "Cashier",
                (int)AccesibilityEnum.OWNER => "Owner",
                (int)AccesibilityEnum.NONE => "None",
                _ => "None"
            };
        }
    }
}