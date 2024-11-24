using Microsoft.AspNetCore.Http;

namespace POS_System.Common.Exceptions
{
    public class InternalServerErrorException : BaseException
    {
        public override int StatusCode => StatusCodes.Status500InternalServerError;
        public override string Title => "Internal server error";
        public override string Details => Message;

        public InternalServerErrorException(string message) : base(message) { }

        public InternalServerErrorException(string message, string parameterName) : base(message)
        {
            ParameterName = parameterName;
        }

        public InternalServerErrorException(string message, string parameterName, Exception inner)
            : base(message, inner)
        {
            ParameterName = parameterName;
        }
    }
}