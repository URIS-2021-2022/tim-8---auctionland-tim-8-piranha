using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using NLog;
using PlotMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PlotMicroservice.ServiceCalls
{
    public class LoggerService
    {
        private readonly IConfiguration Configuration;

        public LoggerService(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public bool LogMessage(LogLevel logLevel, string logMessage, string microserviceName, string microserviceMethod, Exception exception = null)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                string url = Configuration["Services:LoggerService"];

                var log = new LogModel
                {
                    LogLevel = logLevel,
                    LogMessage = logMessage,
                    MicroserviceName = microserviceName,
                    MicroserviceMethod = microserviceMethod,
                    Exception = exception
                };

                HttpContent content = new StringContent(JsonConvert.SerializeObject(log));
                content.Headers.ContentType.MediaType = "application/json";

                HttpResponseMessage response = httpClient.PostAsync(url, content).Result;

                if(!response.IsSuccessStatusCode)
                {
                    return false;
                }

                return true;
            }
        }
    }
}
