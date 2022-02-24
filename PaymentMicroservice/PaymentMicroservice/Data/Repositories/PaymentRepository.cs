using AutoMapper;
using PaymentMicroservice.Data.Interfaces;
using PaymentMicroservice.Entities;
using PaymentMicroservice.Entities.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PaymentMicroservice.Models.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace PaymentMicroservice.Data.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly PaymentContext Context;
        private readonly IMapper Mapper;

        public PaymentRepository(PaymentContext context, IMapper mapper)
        {
            Context = context;
            Mapper = mapper;
        }
        public PaymentConfirmation CreatePayment(Payment Payment)
        {
            var createdEntity = Context.Add(Payment);
            return Mapper.Map<PaymentConfirmation>(createdEntity.Entity);
        }

        public void DeletePayment(Payment payment)
        {
            Context.Remove(payment);
        }

        public List<Payment> GetPayment(string accountNumber = null, string referenceNumber = null)
        {
            return Context.Payment
                .AsNoTracking()
                .Include(a => a.Course)
                .Where(o => (o.AccountNumber == accountNumber || accountNumber == null) && (o.ReferenceNumber == referenceNumber || referenceNumber == null))
                .ToList();
        }

        public Payment GetPaymentById(Guid PaymentId)
        {
            var payment = Context.Payment
                 //.Include(a => a.JavnoNadmetanje)
                 .Include(a => a.Course)
                .FirstOrDefault(o => o.PaymentId == PaymentId);

            if (payment == null)
            {
                throw new NotFoundException(nameof(Payment), PaymentId);
            }

            return payment;
        }

        public void SaveChanges()
        {
            Context.SaveChanges();
        }
    }
}

