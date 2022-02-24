using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PublicBidding.ServiceCalls
{
    public class Service<T> : IService<T>
    {
        private readonly ILoggerService logger;

        public Service(ILoggerService logger)
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

                    await logger.LogMessage(LogLevel.Information, "Communication with other microservice from PublicBidding microservice succeeded!", "PublicBidding microservice", "SendGetRequestAsync");
#pragma warning disable CS8603 // Possible null reference return.
                    return JsonConvert.DeserializeObject<T>(content);
#pragma warning restore CS8603 // Possible null reference return.
                }
#pragma warning disable CS8603 // Possible null reference return.
                return default;
#pragma warning restore CS8603 // Possible null reference return.

            }
            catch (Exception)
            {
                await logger.LogMessage(LogLevel.Error, "Error while trying to communicate with other microservice!", "PublicBidding microservice", "SendGetRequestAsync");
                return default;
            }
        }
    }
}
