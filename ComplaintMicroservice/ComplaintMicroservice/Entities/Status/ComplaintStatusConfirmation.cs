using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintMicroservice.Entities.ComplaintStatusEntities
{
    public class ComplaintStatusConfirmation
    {
        public Guid ComplaintStatusId { get; set; }
        public string Status { get; set; }
    }
}
