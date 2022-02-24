using RegistrationMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistrationMicroservice.Services
{
    public class AuctionMock<T> : IService<T>
    {
        public async Task<T> SendGetRequestAsync(string url)
        {
            var auction = new AuctionDto
            {
                AuctionNum = 1,
                Year = 2022,
                Date = new DateTime(),
                Restriction = 25,
                PriceStep = 13,
                ApplicationDeadline = new DateTime()
            };

            return await Task.FromResult((T)Convert.ChangeType(auction, typeof(T)));

        }
    }
}
