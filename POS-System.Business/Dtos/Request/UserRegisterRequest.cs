namespace POS_System.Business.Dtos.Request
{
    public record UserRegisterRequest(
        string Email,
        string UserName,
        string FirstName,
        string LastName, 
        string Password,
        string PhoneNumber,
        DateOnly BirtDate,
        int RoleId
    );
}