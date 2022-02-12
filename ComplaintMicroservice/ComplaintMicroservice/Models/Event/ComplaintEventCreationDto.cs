using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintMicroservice.Models.Event
{
    public class ComplaintEventCreationDto
    {
        [Required(ErrorMessage = "Must enter an event.")]
        public string Event { get; set; }
    }
}
