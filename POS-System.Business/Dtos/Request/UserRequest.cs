namespace POS_System.Business.Dtos.Request
{
    public record UserRequest(
        string Email,
        string UserName,
        string FirstName,
        string LastName, 
        string Password,
        string PhoneNumber,
        DateTime BirtDate        
    );
}