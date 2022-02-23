namespace AuthMicroservice.Utils.LoggerService
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    /// <summary>
    /// Logger service interface implementation.
    /// </summary>
    public class LoggerService : ILoggerService
    {
        private readonly IConfiguration Configuration;

        /// <summary>
        /// Logger service constructor.
        /// </summary>
        /// <param name="configuration"></param>
        public LoggerService(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Method for logging a message.
        /// </summary>
        /// <param name="logLevel">Log level.</param>
        /// <param name="logMessage">Log message.</param>
        /// <param name="microserviceName">Name of the microservice from which the logger is called.</param>
        /// <param name="microserviceMethod">Method from which the logger is called.</param>
        /// <returns></returns>
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
