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
    /// <summary>
    /// Predstavlja zalbu
    /// </summary>
    public class Complaint
    {
        /// <summary>
        /// ID zalbe
        /// </summary>
        [Key]
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
        /// Tip zalbe
        /// </summary>
        public ComplaintTypeModel ComplaintType { get; set; }
        /// <summary>
        /// ID statusa zalbe
        /// </summary>
        public Guid ComplaintStatusId { get; set; }
        /// <summary>
        /// Status zalbe
        /// </summary>
        public ComplaintStatus ComplaintStatus { get; set; }
        /// <summary>
        /// ID dogadjaja na osnovu zalbe
        /// </summary>
        public Guid ComplaintEventId { get; set; }
        /// <summary>
        /// Dogadjaj na osnovu zalbe
        /// </summary>
        public ComplaintEvent ComplaintEvent { get; set; }
        /// <summary>
        /// Id javnog nadmetanja
        /// </summary>
        public Guid? PublicBiddingId { get; set; }
    }
}
