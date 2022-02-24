using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentMicroservice.Entities
{
    public class CourseConfirmation
    {
        public Guid CourseId { get; set; }

        public string Currency { get; set; }

        public string Value { get; set; }

        public DateTime? CourseDate { get; set; }
    }
}
