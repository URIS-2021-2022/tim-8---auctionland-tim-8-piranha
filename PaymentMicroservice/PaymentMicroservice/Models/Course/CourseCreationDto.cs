using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentMicroservice.Models.Course
{
    public class CourseCreationDto
    {
        public string Currency { get; set; }

        public string Value { get; set; }

        public DateTime? CourseDate { get; set; }
    }
}
