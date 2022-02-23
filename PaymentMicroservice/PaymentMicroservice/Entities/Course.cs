using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentMicroservice.Entities
{
    public class Course
    {
        [Key]
        public Guid CourseId { get; set; }

        public string Currency { get; set; }

        public string Value { get; set; }

        public DateTime? CourseDate { get; set; }

 
    }
}
