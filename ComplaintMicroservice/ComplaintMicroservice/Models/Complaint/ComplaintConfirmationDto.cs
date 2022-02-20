using ComplaintMicroservice.Entities;
using ComplaintMicroservice.Entities.ComplaintStatusEntities;
using ComplaintMicroservice.Entities.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintMicroservice.Models.Complaint
{
    /// <summary>
    /// Potvrda zalbe DTO
    /// </summary>
    public class ComplaintConfirmationDto
    {
        /// <summary>
        /// ID zalbe
        /// </summary>
        public Guid ComplaintId { get; set; }
        /// <summary>
        /// Datum podnosenja zalbe
        /// </summary>
        public DateTime SubmissionDate { get; set; }
        /// <summary>
        /// Razlog zalbe
        /// </summary>
        public string Reason { get; set; }
        /// <summary>
        /// Obrazlozenje
        /// </summary>
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
    }
}
