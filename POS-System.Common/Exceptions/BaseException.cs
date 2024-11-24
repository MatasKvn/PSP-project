using POS_System.Common;

namespace POS_System.Common.Exceptions
{
    public abstract class BaseException : Exception
    {
        public abstract int StatusCode { get; }
        public abstract string Title { get; }
        public abstract string Details { get; }
        public string? ParameterName { get; set;  }

        protected BaseException(string message) : base(message) { }

        protected BaseException(string message, Exception innerException)
            : base(message, innerException) { }

        public virtual ErrorDetails GetErrorDetails() => new()
        {
            Status = StatusCode,
            Title = Title,
            Details = Details,
            ParameterName = ParameterName
        };
    }
}
