using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintMicroservice.Entities
{
    public class ComplaintTypeConfirmation
    {
        public Guid ComplaintTypeId { get; set; }
        public string ComplaintType { get; set; }
    }
}
