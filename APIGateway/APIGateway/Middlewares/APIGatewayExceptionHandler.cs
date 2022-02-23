namespace APIGateway.Middlewares
{
    using Commons.ExceptionHandling;
    using Microsoft.AspNetCore.Http;
    using Newtonsoft.Json;
    using System;
    using System.Net;
    using System.Threading.Tasks;

    public class APIGatewayExceptionHandler
    {
        public static Task HandleExceptionAsync(HttpContext context, BaseException ex)
        {
            if (ex.code == 0)
            {
                ex.code = HttpStatusCode.InternalServerError; 
            }

            var result = JsonConvert.SerializeObject(new
            {
                messsage = ex.Message,
                status = ex.code,
                requested_uri = context.Request.Path,
                timestamp = DateTime.Now
            });

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int) ex.code;

            return context.Response.WriteAsync(result);
        }
    }
}
