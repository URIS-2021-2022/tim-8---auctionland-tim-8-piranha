using Commons.ExceptionHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Common.ExceptionHandling.Exceptions
{
    public class BadCredentialsException : BaseException
    {
        public BadCredentialsException(string message) : base(message, HttpStatusCode.Unauthorized)
        {
            this.message = message;
            code = HttpStatusCode.Unauthorized;
        }
    }
}
