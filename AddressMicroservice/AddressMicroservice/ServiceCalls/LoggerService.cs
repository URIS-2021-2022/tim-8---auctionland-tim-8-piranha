using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AddressMicroservice.Models;
using AddressMicroservice.Models.Log;

namespace DocumentMicroservice.ServiceCalls
{
    public class LoggerService : ILoggerService
    {
        private readonly IConfiguration Configuration;

        public LoggerService(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public async Task<bool> LogMessage(LogLevel logLevel, string logMessage, string microserviceName, string microserviceMethod)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                Uri url = new Uri($"{ Configuration["Services:LoggerService"] }api/logger-service");

                LogModel log = new LogModel
                {
                    LogLevel = logLevel,
                    LogMessage = logMessage,
                    MicroserviceName = microserviceName,
                    MicroserviceMethod = microserviceMethod
                };

                HttpContent content = new StringContent(JsonConvert.SerializeObject(log));
                content.Headers.ContentType.MediaType = "application/json";

                HttpResponseMessage response = httpClient.PostAsync(url, content).Result;

                return await Task.FromResult(response.IsSuccessStatusCode);
            }
        }


    }
}
