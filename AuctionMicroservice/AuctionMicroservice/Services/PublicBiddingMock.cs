using AuctionMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionMicroservice.Services
{
    public class PublicBiddingMock<T> : IService<T>
    {
        public async Task<List<T>> SendGetRequestAsync(string url)
        {
            List<PublicBiddingDto> publicBidding = new List<PublicBiddingDto>();
            publicBidding.Add(new PublicBiddingDto

            {
                Date = new DateTime(),
                StartTime = new DateTime(),
                EndTime = new DateTime(),
                StartPricePerHa = 13,
                IsExcepted = false,
                Price = 250,
                RentPeriod = 3,
                NumberOfApplicants = 21,
                DepositSupplement = 123,
                Round = 2,
                Type = "Test",
                Status = "Test",
                
            });

            return await Task.FromResult((List<T>)Convert.ChangeType(publicBidding, typeof(List<T>)));

        }
    }
}
