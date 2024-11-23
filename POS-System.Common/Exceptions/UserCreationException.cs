namespace POS_System.Common.Exceptions
{
    public class UserCreationException(int statusCode, string message) : Exception(message)
    {
        public int StatusCode => statusCode;
    }
}