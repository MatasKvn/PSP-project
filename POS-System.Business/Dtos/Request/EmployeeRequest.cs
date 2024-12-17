using POS_System.Common.Enums;

namespace POS_System.Business.Dtos.Request
{
    public record EmployeeRequest(
        string FirstName,
        string LastName,
        //DateOnly BirthDate,
        string UserName,
        string Email,
        string PhoneNumber,
        int RoleId
    );
}