namespace Common.ExceptionHandling.Exceptions
{
    using Commons.ExceptionHandling;
    using System;
    using System.Net;
    using System.Runtime.Serialization;

    [Serializable]
    public class BadCredentialsException : BaseException
    {
        protected BadCredentialsException(SerializationInfo info, StreamingContext context) 
            : base(info, context)
        {
        }

        public BadCredentialsException(string message) : base(message, HttpStatusCode.Unauthorized)
        {
            this.message = message;
            code = HttpStatusCode.Unauthorized;
        }
    }
}
