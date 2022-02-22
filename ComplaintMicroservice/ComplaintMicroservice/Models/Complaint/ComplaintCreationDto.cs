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
    /// <summary>
    /// Zalba DTO za kreiranje
    /// </summary>
    public class ComplaintCreationDto
    {
        /// <summary>
        /// Datum podnosenja zalbe
        /// </summary>
        [Required(ErrorMessage = "Must enter a date.")]
        public DateTime SubmissionDate { get; set; }
        /// <summary>
        /// Razlog zalbe
        /// </summary>
        [Required(ErrorMessage = "Must enter a reason.")]
        public string Reason { get; set; }
        /// <summary>
        /// Obrazlozenje
        /// </summary>
        [Required(ErrorMessage = "Must enter a explanation.")]
        public string Explanation { get; set; }
        /// <summary>
        /// Broj resenja
        /// </summary>
        public string SolutionNumber { get; set; }
        /// <summary>
        /// Broj odluke
        /// </summary>
        public string DecisionNumber { get; set; }
        /// <summary>
        /// ID tipa zalbe
        /// </summary>
        public Guid ComplaintTypeId { get; set; }
        /// <summary>
        /// ID statusa zalbe
        /// </summary>
        public Guid ComplaintStatusId { get; set; }
        /// <summary>
        /// ID dogadjaja na osnovu zalbe
        /// </summary>
        public Guid ComplaintEventId { get; set; }
        /// <summary>
        /// Id javnog nadmetanja
        /// </summary>
        public Guid PublicBiddingId { get; set; }
    }
}
