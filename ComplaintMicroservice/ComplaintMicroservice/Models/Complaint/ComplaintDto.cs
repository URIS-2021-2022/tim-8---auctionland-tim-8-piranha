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
    /// <summary>
    /// Zalba DTO
    /// </summary>
    public class ComplaintDto
    {
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
        /// DTO tipa zalbe
        /// </summary>
        public ComplaintTypeDto ComplaintType { get; set; }
        /// <summary>
        /// DTO statusa zalbe
        /// </summary>
        public ComplaintMicroservice.Models.ComplaintStatusDto.ComplaintStatusDto ComplaintStatus { get; set; }
        /// <summary>
        /// DTO dogadjaja na osnovu zalbe
        /// </summary>
        public ComplaintEventDto ComplaintEvent { get; set; }
        /// <summary>
        /// Javnog nadmetanje
        /// </summary>
        public PublicBiddingDto PublicBidding { get; set; }
    }
}
