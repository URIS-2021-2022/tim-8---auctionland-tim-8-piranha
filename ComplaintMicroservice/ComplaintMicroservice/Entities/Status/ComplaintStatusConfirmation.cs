using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintMicroservice.Entities.ComplaintStatusEntities
{
    /// <summary>
    /// Predstavlja potvrdu statusa zalbe
    /// </summary>
    public class ComplaintStatusConfirmation
    {
        /// <summary>
        /// ID statusa zalbe
        /// </summary>
        public Guid ComplaintStatusId { get; set; }
        /// <summary>
        /// Status zalbe
        /// </summary>
        public string Status { get; set; }
    }
}
