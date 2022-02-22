using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AuctionMicroservice.Services
{
    public class ServiceCall<T> : IService<T>
    {
        //private readonly ILoggerService logger;

        //public ServiceCall(ILoggerService logger)
        //{
        //    this.logger = logger;
        //}
        public async Task<List<T>> SendGetRequestAsync(string url)
        {
            try
            {
                using HttpClient httpClient = new HttpClient();

                var request = new HttpRequestMessage(HttpMethod.Get, url);

                request.Headers.Add("Accept", "application/json");

                var response = await httpClient.SendAsync(request);

                if(response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    if(string.IsNullOrEmpty(content))
                    {
                        return default;
                    }

                    return JsonConvert.DeserializeObject<List<T>>(content);
                }

                return default;


            }catch(Exception)
            {
                return default;
            }
        }
    }
}
