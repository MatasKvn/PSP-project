namespace POS_System.Business.Dtos.Request
{
    public record UserRegisterRequest(
        string Email,
        string UserName,
        string FirstName,
        string LastName, 
        string Password,
        string PhoneNumber,
        DateTime BirtDate      
    );
}