namespace POS_System.Business.Dtos.Request
{
    public record UserLoginRequest(
        string UserName,
        string Password
    );
}