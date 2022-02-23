namespace Common.ExceptionHandling.Exceptions
{
    using Commons.ExceptionHandling;
    using System.Net;

    public class EntityCannotBeSavedException : BaseException
    {
        public EntityCannotBeSavedException(string message) : base(message, HttpStatusCode.Conflict)
        {
            this.message = message;
            code = HttpStatusCode.Conflict;
        }
    }
}
