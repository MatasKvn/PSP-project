using Microsoft.AspNetCore.Http;

namespace POS_System.Common.Exceptions
{
    public class UnauthorizedException : BaseException
    {
        public override int StatusCode => StatusCodes.Status401Unauthorized;
        public override string Title => "Unauthorized";
        public override string Details => Message;

        public UnauthorizedException(string message) : base(message) { }

        public UnauthorizedException(string message, string parameterName) : base(message)
        {
            ParameterName = parameterName;
        }

        public UnauthorizedException(string message, string parameterName, Exception inner)
            : base(message, inner)
        {
            ParameterName = parameterName;
        }
    }
}