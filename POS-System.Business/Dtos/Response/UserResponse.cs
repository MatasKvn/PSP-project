namespace POS_System.Business.Dtos.Response
{
    public record UserResponse(
        int Id,
        string UserName,
        string FirstName,
        string LastName,
        string PhoneNumber,
        string Email,
        DateOnly BirthDate,
        DateOnly StartDate,
        DateOnly EndDate,
        DateTime Version,
        bool IsDeleted
    );
}