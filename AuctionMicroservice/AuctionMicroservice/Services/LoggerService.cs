using AuctionMicroservice.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AuctionMicroservice.Services
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

                var log = new LogModel
                {
                    LogLevel = logLevel,
                    LogMessage = logMessage,
                    MicroserviceName = microserviceName,
                    MicroserviceMethod = microserviceMethod
                };

                HttpContent content = new StringContent(JsonConvert.SerializeObject(log));
                content.Headers.ContentType.MediaType = "application/json";

                HttpResponseMessage response = httpClient.PostAsync(url, content).Result;

                if (!response.IsSuccessStatusCode)
                {
                    return false;
                }

                 return true;
            }
        }
    }
}
