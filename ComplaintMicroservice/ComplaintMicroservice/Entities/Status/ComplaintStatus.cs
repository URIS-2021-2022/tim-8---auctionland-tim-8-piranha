using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintMicroservice.Entities.ComplaintStatusEntities
{
    public class ComplaintStatus
    {
        [Key]
        public Guid ComplaintStatusId { get; set; }
        public string Status { get; set; }
    }
}
