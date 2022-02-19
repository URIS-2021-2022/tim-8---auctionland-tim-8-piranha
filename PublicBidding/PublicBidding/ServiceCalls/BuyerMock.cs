using PublicBidding.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicBidding.ServiceCalls
{
    public class BuyerMock<T> : IService<T>
    {
        public async Task<T> SendGetRequestAsync()
        {
            var buyer = new BuyerDto
            {
                Name = "Davor Jelic",
                Email = "davorjelic2334@gmail.com",
                AccountNumber = "1212412",
                PhoneNumber = "063141241"
            };

            return await Task.FromResult((T)Convert.ChangeType(buyer, typeof(T)));
        }
    }
}
