using Microsoft.AspNetCore.Http;

namespace POS_System.Domain.Exceptions;

public class NotFoundException : BaseException
{
    public override int StatusCode => StatusCodes.Status404NotFound;
    public override string Title => "Not Found";
    public override string Details => Message;

    public NotFoundException(string message) : base(message) { }

    public NotFoundException(string message, string parameterName) : base(message)
    {
        ParameterName = parameterName;
    }

    public NotFoundException(string message, string parameterName, Exception innerException)
        : base(message, innerException)
    {
        ParameterName = parameterName;
    }
}