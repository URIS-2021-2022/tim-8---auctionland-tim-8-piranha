namespace Common.ExceptionHandling.Exceptions
{
    using Commons.ExceptionHandling;
    using System;
    using System.Net;
    using System.Runtime.Serialization;

    [Serializable]
    public class EntityNotFoundException : BaseException
    {
        public EntityNotFoundException(string message) : base(message, HttpStatusCode.NotFound)
        {
            this.message = message;
            code = HttpStatusCode.NotFound;
        }

        protected EntityNotFoundException(SerializationInfo info, StreamingContext context) 
            : base(info, context)
        {
        }
    }
}
