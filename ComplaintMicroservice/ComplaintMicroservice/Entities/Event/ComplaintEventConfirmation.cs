using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintMicroservice.Entities.Event
{
    public class ComplaintEventConfirmation
    {
        public Guid ComplaintEventId { get; set; }
        public string Event { get; set; }
    }
}
