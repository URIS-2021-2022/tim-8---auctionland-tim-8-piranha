using RegistrationMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistrationMicroservice.Services
{
    public class BuyerMock<T> : IService<T>
    {
        public async Task<T> SendGetRequestAsync(string url)
        {
            var auction = new BuyerDto
            {
                BuyerName = "Panic Luka",
                BuyerAddress = "Pap Pavla 12",
                BuyerPhoneNumber = "0603704892",
                BuyerAccountNumber = "1234567890",
                BuyerEmail ="lukap181@gmail.com"
            };

            return await Task.FromResult((T)Convert.ChangeType(auction, typeof(T)));

        }
    }
}
