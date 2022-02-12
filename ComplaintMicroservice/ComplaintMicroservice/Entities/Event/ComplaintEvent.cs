using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintMicroservice.Entities.Event
{
    public class ComplaintEvent
    {
        [Key]
        public Guid ComplaintEventId { get; set; }
        public string Event { get; set; }
    }
}
