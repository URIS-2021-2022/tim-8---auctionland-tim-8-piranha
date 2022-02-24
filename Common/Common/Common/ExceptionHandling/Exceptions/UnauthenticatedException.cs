namespace Common.ExceptionHandling.Exceptions
{
    using Commons.ExceptionHandling;
    using System;
    using System.Net;
    using System.Runtime.Serialization;

    [Serializable]
    public class UnauthenticatedException : BaseException
    {
        public UnauthenticatedException(string message) : base(message, HttpStatusCode.Unauthorized)
        {
            this.message = message;
            code = HttpStatusCode.Unauthorized;
        }

        protected UnauthenticatedException(SerializationInfo info, StreamingContext context) 
            : base(info, context)
        {
        }
    }
}
