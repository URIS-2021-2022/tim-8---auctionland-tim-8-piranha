using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintMicroservice.Models.Event
{
    public class ComplaintEventUpdateDto
    {
        public Guid ComplaintEventId { get; set; }

        [Required(ErrorMessage = "Must enter an event.")]
        public string Event { get; set; }
    }
}
