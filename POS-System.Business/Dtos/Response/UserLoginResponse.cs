namespace POS_System.Business.Dtos.Response
{
    public record UserLoginResponse(
        string UserName,
        string JwtToken
    );
}