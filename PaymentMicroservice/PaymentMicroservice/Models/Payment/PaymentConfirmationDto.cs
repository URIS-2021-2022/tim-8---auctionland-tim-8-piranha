using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentMicroservice.Models.Payment
{
    public class PaymentConfirmationDto
    {
        public string AccountNumber { get; set; }

        public string ReferenceNUmber { get; set; }

        public Guid CourseID { get; set; }

        public Guid PublicBiddingId { get; set; }
    }
}
