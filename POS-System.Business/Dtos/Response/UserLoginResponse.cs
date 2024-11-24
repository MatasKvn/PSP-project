namespace POS_System.Business.Dtos.Response
{
    public record UserLoginResponse(
        int Id,
        string UserName,
        string Role,
        string JwtToken
    );
}