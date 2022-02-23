using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentMicroservice.Models.Payment
{
    public class PaymentDto
    {
        public Guid PaymentId { get; set; }

        public string AccountNumber { get; set; }

        public string ReferenceNUmber { get; set; }

        public decimal Amount { get; set; }

        public string PurposeOfPayment { get; set; }

        public DateTime? PaymentDate { get; set; }

        public Entities.Course Course { get; set; }

        public PublicBiddingDto publicBinding { get; set; }

    }
}