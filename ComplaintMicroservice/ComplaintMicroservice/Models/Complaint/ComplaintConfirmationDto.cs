using ComplaintMicroservice.Entities;
using ComplaintMicroservice.Entities.ComplaintStatusEntities;
using ComplaintMicroservice.Entities.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintMicroservice.Models.Complaint
{
    public class ComplaintConfirmationDto
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
