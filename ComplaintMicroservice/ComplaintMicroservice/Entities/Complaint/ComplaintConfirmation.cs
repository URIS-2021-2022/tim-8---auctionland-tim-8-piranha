using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintMicroservice.Entities.Complaint
{
    /// <summary>
    /// Potvrda zalbe
    /// </summary>
    public class ComplaintConfirmation
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
        /// Broj odluke
        /// </summary>
        public string SolutionNumber { get; set; }
        /// <summary>
        /// Broj resenja
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
        /// <summary>
        /// Id kupca
        /// </summary>
        public Guid BuyerId { get; set; }
    }
}
