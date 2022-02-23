using BuyerMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyerMicroservice.ServiceCalls.Mocks
{
    public class AddressServiceCallMock<T> : IServiceCall<T>
    {
        public async Task<T> SendGetRequestAsync(string url)
        {
            AddressDto address = new AddressDto
            {
                //addressId = Guid.Parse("9d4f8183-7c31-4f1a-93e5-cf6fa3b8f8fe"),
                street = "Mira Popare",
                numberStreet = "8",
                place = "Secanj",
                zipCode = "23240"
                
            };

            return await Task.FromResult((T)Convert.ChangeType(address, typeof(T)));
        }
           
    }
}
