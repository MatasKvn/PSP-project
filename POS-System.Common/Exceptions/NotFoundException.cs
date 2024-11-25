using Microsoft.AspNetCore.Http;

namespace POS_System.Common.Exceptions
{
    public class NotFoundException : BaseException
    {
        public override int StatusCode => StatusCodes.Status404NotFound;
        public override string Title => "Not found";
        public override string Details => Message;

        public NotFoundException(string message) : base(message) { }

        public NotFoundException(string message, string parameterName) : base(message)
        {
            ParameterName = parameterName;
        }

        public NotFoundException(string message, string parameterName, Exception inner)
            : base(message, inner)
        {
            ParameterName = parameterName;
        }
    }
}