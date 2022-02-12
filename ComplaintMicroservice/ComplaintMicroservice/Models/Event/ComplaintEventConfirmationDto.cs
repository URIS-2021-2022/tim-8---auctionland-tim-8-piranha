using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintMicroservice.Models.Event
{
    public class ComplaintEventConfirmationDto
    {
        public Guid ComplaintEventId { get; set; }
        public string Event { get; set; }
    }
}
