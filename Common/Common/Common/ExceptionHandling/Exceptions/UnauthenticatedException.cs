namespace Common.ExceptionHandling.Exceptions
{
    using Commons.ExceptionHandling;
    using System.Net;

    public class UnauthenticatedException : BaseException
    {
        public UnauthenticatedException(string message) : base(message, HttpStatusCode.Unauthorized)
        {
            this.message = message;
            code = HttpStatusCode.Unauthorized;
        }
    }
}
