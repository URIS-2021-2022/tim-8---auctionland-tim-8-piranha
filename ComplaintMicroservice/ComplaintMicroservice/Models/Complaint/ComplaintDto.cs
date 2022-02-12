using ComplaintMicroservice.Entities;
using ComplaintMicroservice.Entities.ComplaintStatusEntities;
using ComplaintMicroservice.Entities.Event;
using ComplaintMicroservice.Models.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintMicroservice.Models.Complaint
{
    public class ComplaintDto
    {
        public DateTime SubmissionDate { get; set; }
        public string Reason { get; set; }
        public string Explanation { get; set; }
        public string SolutionNumber { get; set; }
        public string DecisionNumber { get; set; }
        public ComplaintTypeDto ComplaintType { get; set; }
        public ComplaintMicroservice.Models.ComplaintStatusDto.ComplaintStatusDto ComplaintStatus { get; set; }
        public ComplaintEventDto ComplaintEvent { get; set; }
    }
}
