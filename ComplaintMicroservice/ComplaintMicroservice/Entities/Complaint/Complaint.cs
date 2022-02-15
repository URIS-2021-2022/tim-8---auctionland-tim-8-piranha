using ComplaintMicroservice.Entities.ComplaintStatusEntities;
using ComplaintMicroservice.Entities.Event;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintMicroservice.Entities.Complaint
{
    public class Complaint
    {
        [Key]
        public Guid ComplaintId { get; set; }
        public DateTime SubmissionDate { get; set; }
        public string Reason { get; set; }
        public string Explanation { get; set; }
        public string SolutionNumber { get; set; }
        public string DecisionNumber { get; set; }
        public Guid ComplaintTypeId { get; set; }
        public ComplaintTypeModel ComplaintType { get; set; }
        public Guid ComplaintStatusId { get; set; }
        public ComplaintStatus ComplaintStatus { get; set; }
        public Guid ComplaintEventId { get; set; }
        public ComplaintEvent ComplaintEvent { get; set; }

    }
}
