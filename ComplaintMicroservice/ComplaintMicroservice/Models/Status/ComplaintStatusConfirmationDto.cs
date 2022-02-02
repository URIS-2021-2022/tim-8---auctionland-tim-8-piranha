using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintMicroservice.Models.ComplaintStatusDto
{
    public class ComplaintStatusConfirmationDto
    {
        public Guid ComplaintStatusId { get; set; }
        public string Status { get; set; }
    }
}
