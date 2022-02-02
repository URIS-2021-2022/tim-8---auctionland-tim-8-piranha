using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintMicroservice.Models.ComplaintStatusDto
{
    public class ComplaintStatusUpdateDto
    {
        public Guid ComplaintStatusId { get; set; }

        [Required(ErrorMessage = "Must enter a type.")]
        public string Status { get; set; }
    }
}
