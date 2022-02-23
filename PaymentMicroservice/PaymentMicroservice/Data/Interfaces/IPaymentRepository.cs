using PaymentMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentMicroservice.Data.Interfaces
{
    public interface IPaymentRepository
    {
        List<Payment> GetPayment(string accountNumber = null, string referenceNumber = null);

        Payment GetPaymentById(Guid paymentId);

        PaymentConfirmation CreatePayment(Payment payment);

        void DeletePayment(Payment PaymentId);

        void SaveChanges();
    }
}
