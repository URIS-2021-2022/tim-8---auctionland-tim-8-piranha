using DocumentMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMicroservice.ServiceCalls.Mocks
{
    public class AuctionServiceCallMock<T> : IServiceCall<T>
    {
        public async Task<T> SendGetRequestAsync(string url)
        {
            AuctionDto auction = new AuctionDto
            {
                AuctionNum = 126,
                Year = 2,
                Date = new DateTime(),
                Restriction = 5,
                PriceStep = 2,
                ApplicationDeadline= new DateTime()
            };

            return await Task.FromResult((T)Convert.ChangeType(auction, typeof(T)));
        }
    }
}
