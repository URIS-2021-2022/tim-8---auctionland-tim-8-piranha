using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintMicroservice.Entities.Event
{
    /// <summary>
    /// Predstavlja potvrdu dogadjaja na osnovu zalbe
    /// </summary>
    public class ComplaintEventConfirmation
    {
        /// <summary>
        /// ID dogadjaja
        /// </summary>
        public Guid ComplaintEventId { get; set; }
        /// <summary>
        /// Dogadjaj na osnovu zalbe
        /// </summary>
        public string Event { get; set; }
    }
}
