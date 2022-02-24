namespace Common.ExceptionHandling.Exceptions
{
    using Commons.ExceptionHandling;
    using System.Net;

    public class EntityAlreadyExistsException : BaseException
    {
        public EntityAlreadyExistsException(string message) : base(message, HttpStatusCode.Conflict)
        {
            this.message = message;
            this.code = HttpStatusCode.Conflict;
        }
    }
}
