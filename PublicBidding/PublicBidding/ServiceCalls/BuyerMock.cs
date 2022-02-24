using PublicBidding.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicBidding.ServiceCalls
{
    public class BuyerMock<T> : IService<T>
    {
        public async Task<T> SendGetRequestAsync(string url)
        {
            var buyer = new BuyerDto
            {
                name = "Davor Jelic",
                accountNumber = "1212412",
                phone1 = "063141241"
            };

            return await Task.FromResult((T)Convert.ChangeType(buyer, typeof(T)));
        }
    }
}
