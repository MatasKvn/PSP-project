using POS_System.Common.Enums;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace POS_System.Business.Dtos.Response
{
    public record EmployeeResponse(
        int Id,
        string FirstName,
        string LastName,
        DateOnly BirthDate,
        DateOnly StartDate,
        DateOnly EndDate,
        string UserName,
        string Email,
        string PhoneNumber,
        DateTime Version,
        bool IsDeleted
        //AccesibilityEnum Accessibility
    );
}