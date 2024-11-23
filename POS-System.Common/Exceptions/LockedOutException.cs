using System.Net;

namespace POS_System.Common.Exceptions
{
    public class LockedOutException(int statusCode, string message) : Exception(message)
    {
        public int StatusCode => statusCode;
    }
}