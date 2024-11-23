namespace POS_System.Common.Exceptions
{
    public class InvalidSignInCredsException(int statusCode, string message) : Exception(message)
    {
        public int StatusCode => statusCode;
    }
}