namespace Common.ExceptionHandling.Exceptions
{
    using Commons.ExceptionHandling;
    using System;
    using System.Net;
    using System.Runtime.Serialization;

    [Serializable]
    public class EntityCannotBeSavedException : BaseException
    {
        public EntityCannotBeSavedException(string message) : base(message, HttpStatusCode.Conflict)
        {
            this.message = message;
            code = HttpStatusCode.Conflict;
        }

        protected EntityCannotBeSavedException(SerializationInfo info, StreamingContext context) 
            : base(info, context)
        {
        }
    }
}
