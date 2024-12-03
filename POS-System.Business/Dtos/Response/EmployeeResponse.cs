namespace POS_System.Business.Dtos.Response
{
    public record EmployeeResponse(
        int Id,
        string FirstName,
        string LastName,
        DateOnly BirthDate,
        DateOnly EndDate,
        string UserName,
        string Email,
        string PhoneNumber,
        DateTime Version,
        bool IsDeleted
    );
}