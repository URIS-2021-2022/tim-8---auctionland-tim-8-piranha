using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PublicBidding.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PublicBidding.ServiceCalls
{
    public class LoggerService : ILoggerService
    {
        private readonly IConfiguration configuration;

        public LoggerService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<bool> LogMessage(LogLevel logLevel, string logMessage, string microserviceName, string microserviceMethod)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                Uri url = new Uri($"{ configuration["Services:LoggerService"] }api/logger-service");

                LogModel log = new LogModel
                {
                    LogLevel = logLevel,
                    LogMessage = logMessage,
                    MicroserviceName = microserviceName,
                    MicroserviceMethod = microserviceMethod
                };

                HttpContent content = new StringContent(JsonConvert.SerializeObject(log));
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                content.Headers.ContentType.MediaType = "application/json";
#pragma warning restore CS8602 // Dereference of a possibly null reference.

                HttpResponseMessage response = httpClient.PostAsync(url, content).Result;

                return await Task.FromResult(response.IsSuccessStatusCode);
            }
        }
    }
}
