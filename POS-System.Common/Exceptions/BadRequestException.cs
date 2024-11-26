using Microsoft.AspNetCore.Http;

namespace POS_System.Domain.Exceptions;

public class BadRequestException : BaseException
{
    public override int StatusCode => StatusCodes.Status400BadRequest;
    public override string Title => "Bad request";
    public override string Details => Message;

    public BadRequestException(string message) : base(message) { }

    public BadRequestException(string message, string parameterName) : base(message)
    {
        ParameterName = parameterName;
    }

    public BadRequestException(string message, string parameterName, Exception innerException)
        : base(message, innerException)
    {
        ParameterName = parameterName;
    }
}
