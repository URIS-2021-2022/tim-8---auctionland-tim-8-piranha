using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentMicroservice.Entities
{
    public class Payment
    {
        [Key]
        public Guid PaymentId { get; set; }

        public string AccountNumber { get; set; }

        public string ReferenceNumber { get; set; }

        public decimal Amount { get; set; }

        public string PurposeOfPayment { get; set; }

        public DateTime? PaymentDate { get; set; }

        [ForeignKey("Course")] 
        public Guid CourseId { get; set; }
        public Course Course { get; set; }
        
        public Guid PublicBiddingId { get; set; }

        /*ForeignKey("State")]  ------------------------javno nadmetanje
        public Guid StateID { get; set; }
        public State State { get; set; }
         */
    }
}