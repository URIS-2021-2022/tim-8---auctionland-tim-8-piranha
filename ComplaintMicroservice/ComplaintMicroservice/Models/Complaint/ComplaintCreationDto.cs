using ComplaintMicroservice.Entities;
using ComplaintMicroservice.Entities.ComplaintStatusEntities;
using ComplaintMicroservice.Entities.Event;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintMicroservice.Models.Complaint
{
    public class ComplaintCreationDto
    {
        [Required(ErrorMessage = "Must enter a date.")]
        public DateTime SubmissionDate { get; set; }
        [Required(ErrorMessage = "Must enter a reason.")]
        public string Reason { get; set; }
        [Required(ErrorMessage = "Must enter a explanation.")]
        public string Explanation { get; set; }
        public string SolutionNumber { get; set; }
        public string DecisionNumber { get; set; }
        public Guid ComplaintTypeId { get; set; }
        public Guid ComplaintStatusId { get; set; }
        public Guid ComplaintEventId { get; set; }
    }
}
