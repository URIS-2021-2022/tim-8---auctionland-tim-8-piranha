namespace Common.ExceptionHandling.Exceptions
{
    using Commons.ExceptionHandling;
    using System.Net;

    public class EntityNotFoundException : BaseException
    {
        public EntityNotFoundException(string message) : base(message, HttpStatusCode.NotFound)
        {
            this.message = message;
            code = HttpStatusCode.NotFound;
        }
    }
}
