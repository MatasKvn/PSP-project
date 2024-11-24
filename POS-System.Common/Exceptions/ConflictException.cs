using Microsoft.AspNetCore.Http;

namespace POS_System.Common.Exceptions
{
    public class ConflictException : BaseException
    {
        public override int StatusCode => StatusCodes.Status409Conflict;
        public override string Title => "Conflict";
        public override string Details => Message;

        public ConflictException(string message) : base(message) { }

        public ConflictException(string message, string parameterName) : base(message)
        {
            ParameterName = parameterName;
        }

        public ConflictException(string message, string parameterName, Exception inner)
            : base(message, inner)
        {
            ParameterName = parameterName;
        }
    }
}