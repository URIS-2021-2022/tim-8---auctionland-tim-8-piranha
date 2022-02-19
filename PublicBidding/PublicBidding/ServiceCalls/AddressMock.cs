using Microsoft.AppCenter.Ingestion;
using PublicBidding.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicBidding.ServiceCalls
{
    public class AddressMock<T> : IService<T>
    {
        public async Task<T> SendGetRequestAsync()
        {
            var address = new AddressDto
            {
                Street = "Save Kovacevica",
                AddressNumber = "15",
                City = "Novi Sad",
                PostalCode = 21000,
                Country = "Srbija"
            };

            return await Task.FromResult((T)Convert.ChangeType(address, typeof(T)));
        }
    }
}
