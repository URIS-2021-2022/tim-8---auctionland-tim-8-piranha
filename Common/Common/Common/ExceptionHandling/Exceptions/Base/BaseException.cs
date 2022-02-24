namespace Commons.ExceptionHandling
{
    using System;
    using System.Net;

    public class BaseException : Exception
    {

        public string message { get; set; }

        public HttpStatusCode code { get; set; }

        public override string Message
        {
            get { return this.message; }
        }

        public BaseException(string message, HttpStatusCode code)
        {
            this.message = message;
            this.code = code;
        }
    }
}
