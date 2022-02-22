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

                    //await Logger.LogMessage(LogLevel.Information, "Communication with Buyer microservice succeeded!", "Plot microservice", "SendGetRequestAsync");
                    return JsonConvert.DeserializeObject<T>(content);
                }
                return default;

            }
            catch (Exception)
            {
                //await Logger.LogMessage(LogLevel.Error, "Error while trying to communicate with Buyer microservice!", "Plot microservice", "SendGetRequestAsync");
                return default;
            }
        }
    }
}
