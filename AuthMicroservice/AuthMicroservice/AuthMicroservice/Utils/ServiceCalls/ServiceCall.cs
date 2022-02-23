namespace AuthMicroservice.Utils.ServiceCalls
{
    using AuthMicroservice.Consts;
    using AuthMicroservice.Utils.LoggerService;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    /// <summary>
    /// Service call interface implementation.
    /// </summary>
    /// <typeparam name="T">Type of the service.</typeparam>
    public class ServiceCall<T> : IServiceCall<T>
    {
        private readonly ILoggerService Logger;

        /// <summary>
        /// Constructor for declaring logger service as a service to which the requests are made.
        /// </summary>
        /// <param name="logger"></param>
        public ServiceCall(ILoggerService logger)
        {
            Logger = logger;
        }

        /// <summary>
        /// Method used for sending a request to a service.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
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

                    await Logger.LogMessage(LogLevel.Information, "Communication with microservice succeeded!", GeneralConsts.MICROSERVICE_NAME, "SendGetRequestAsync");
                    return JsonConvert.DeserializeObject<T>(content);
                }
                return default;

            }
            catch (Exception)
            {
                await Logger.LogMessage(LogLevel.Error, "Error while trying to communicate with microservice!", GeneralConsts.MICROSERVICE_NAME, "SendGetRequestAsync");
                return default;
            }
        }
    }
}
