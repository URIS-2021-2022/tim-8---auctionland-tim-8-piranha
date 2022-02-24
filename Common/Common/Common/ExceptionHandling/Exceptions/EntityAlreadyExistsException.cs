namespace Common.ExceptionHandling.Exceptions
{
    using Commons.ExceptionHandling;
    using System.Net;
    using System.Runtime.Serialization;

    public class EntityAlreadyExistsException : BaseException
    {
        public EntityAlreadyExistsException(string message) : base(message, HttpStatusCode.Conflict)
        {
            this.message = message;
            code = HttpStatusCode.Conflict;
        }

        protected EntityAlreadyExistsException(SerializationInfo info, StreamingContext context) 
            : base(info, context)
        {
        }
    }
}
