using DocumentMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMicroservice.ServiceCalls.Mocks
{
    public class BuyerServiceCallMock<T> : IServiceCall<T>
    {
        public async Task<T> SendGetRequestAsync(string url)
        {
            BuyerDto buyer = new BuyerDto
            {
                BuyerName = "Andrija Pavlov",
                BuyerAddress = "Pap Pavla 15",
                BuyerPhoneNumber = "0603704892",
                BuyerAccountNumber = "1234567890",
                BuyerEmail = "pavlovandrija9@gmail.com"
            };

            return await Task.FromResult((T)Convert.ChangeType(buyer, typeof(T)));
        }
    }
}
