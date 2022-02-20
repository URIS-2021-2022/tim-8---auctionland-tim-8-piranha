using PlotMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.ServiceCalls.Mocks
{
    public class BuyerServiceCallMock<T> : IServiceCall<T>
    {
        public async Task<T> SendGetRequestAsync(string url)
        {
            BuyerDto buyer = new BuyerDto
            {
                BuyerName = "Andrija Pavlov"
            };

            return await Task.FromResult((T)Convert.ChangeType(buyer, typeof(T)));
        }
    }
}
