using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintMicroservice.Entities.Complaint
{
    public class ComplaintConfirmation
    {
        public Guid ComplaintId { get; set; }
        public DateTime SubmissionDate { get; set; }
        public string Reason { get; set; }
        public string Explanation { get; set; }
        public string SolutionNumber { get; set; }
        public string DecisionNumber { get; set; }
        public Guid ComplaintTypeId { get; set; }
        public Guid ComplaintStatusId { get; set; }
        public Guid ComplaintEventId { get; set; }
    }
}
