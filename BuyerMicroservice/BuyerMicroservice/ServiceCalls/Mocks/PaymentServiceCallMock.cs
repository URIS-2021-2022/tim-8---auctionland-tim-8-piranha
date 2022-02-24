using BuyerMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyerMicroservice.ServiceCalls.Mocks
{
    public class PaymentServiceCallMock<T> : IServiceCall<T>
    {
        public async Task<T> SendGetRequestAsync(string url)
        {
            PaymentDto payment = new PaymentDto
            {
                //paymentId = Guid.Parse("0ed644da-bfc1-475b-8694-051f2af885f0"),
                accountNumber = "432432432",
                referenceNumber = "234214213",
                amount = 1000,
                purposeOfPayment = "32432",

            };

            return await Task.FromResult((T)Convert.ChangeType(payment, typeof(T)));
        }
    }
}
