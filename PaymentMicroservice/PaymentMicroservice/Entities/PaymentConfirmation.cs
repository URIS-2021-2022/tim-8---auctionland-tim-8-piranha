using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentMicroservice.Entities
{
    public class PaymentConfirmation
    {
        public Guid PaymentId { get; set; }

        public string AccountNumber { get; set; }

        public string ReferenceNUmber { get; set; }

        public decimal Amount { get; set; }

        public string PurposeOfPayment { get; set; }

        public DateTime? PaymentDate { get; set; }

        public Guid CourseId { get; set; }
    }
}
