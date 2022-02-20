using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintMicroservice.Models.Event
{

    /// <summary>
    /// Potvrda dogadjaja na osnovu zalbe DTO
    /// </summary>
    public class ComplaintEventConfirmationDto
    {

        /// <summary>
        /// ID dogadjaja na osnovu zalbe 
        /// </summary>
        public Guid ComplaintEventId { get; set; }

        /// <summary>
        /// Dogadjaj na osnovu zalbe 
        /// </summary>
        public string Event { get; set; }
    }
}
