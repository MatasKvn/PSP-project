using Microsoft.AspNetCore.Http;

namespace POS_System.Domain.Exceptions;

public class NotFoundException(string message) : BaseException(message)
{
    public override int StatusCode => StatusCodes.Status404NotFound;
    public override string Title => "Not Found";
    public override string Detail => Message;
}