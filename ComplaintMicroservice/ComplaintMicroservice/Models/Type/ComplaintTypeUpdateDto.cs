using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintMicroservice.Models
{
    public class ComplaintTypeUpdateDto
    {
        public Guid ComplaintTypeId { get; set; }

        [Required(ErrorMessage = "Must enter a type.")]
        public string ComplaintType { get; set; }
    }
}
