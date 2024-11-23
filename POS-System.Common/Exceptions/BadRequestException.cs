using Microsoft.AspNetCore.Http;

namespace POS_System.Domain.Exceptions;

public class BadRequestException(string message) : BaseException(message)
{
    public override int StatusCode => StatusCodes.Status400BadRequest;
    public override string Title => "Bad request";
    public override string Detail => Message;
}
