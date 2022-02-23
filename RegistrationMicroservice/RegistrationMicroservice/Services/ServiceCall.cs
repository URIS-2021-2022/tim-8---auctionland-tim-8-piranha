using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace RegistrationMicroservice.Services
{
    public class ServiceCall<T> : IService<T>
    {
        private readonly ILoggerService logger;

        public ServiceCall(ILoggerService logger)
        {
            this.logger = logger;
        }
        public async Task<T> SendGetRequestAsync(string url)
        {
            try
            {
                using var httpClient = new HttpClient();

                var request = new HttpRequestMessage(HttpMethod.Get, url);
                request.Headers.Add("Accept", "application/json");

                var response = await httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    if (string.IsNullOrEmpty(content))
                    {
                        return default;
                    }

                    await logger.LogMessage(LogLevel.Information, "Communication with Buyer microservice succeeded!", "Plot microservice", "SendGetRequestAsync");
                    return JsonConvert.DeserializeObject<T>(content);
                }
                return default;

            }
            catch (Exception)
            {
                await logger.LogMessage(LogLevel.Error, "Error while trying to communicate with Buyer microservice!", "Plot microservice", "SendGetRequestAsync");
                return default;
            }
        }
    }
}
