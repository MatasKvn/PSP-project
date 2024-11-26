using Microsoft.AspNetCore.Http;

namespace POS_System.Common.Exceptions
{
    public class TooManyRequestsException : BaseException
    {
        public override int StatusCode => StatusCodes.Status429TooManyRequests;
        public override string Title => "Too many requests";
        public override string Details => Message;

        public TooManyRequestsException(string message) : base(message) { }

        public TooManyRequestsException(string message, string parameterName) : base(message)
        {
            ParameterName = parameterName;
        }

        public TooManyRequestsException(string message, string parameterName, Exception inner)
            : base(message, inner)
        {
            ParameterName = parameterName;
        }
    }
}