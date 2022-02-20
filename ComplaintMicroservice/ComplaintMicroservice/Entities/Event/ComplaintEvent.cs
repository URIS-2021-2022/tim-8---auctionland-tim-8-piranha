using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintMicroservice.Entities.Event
{
    /// <summary>
    /// Predstavlja dogadjaj na osnovu zalbe
    /// </summary>
    public class ComplaintEvent
    {
        /// <summary>
        /// ID dogadjaja
        /// </summary>
        [Key]
        public Guid ComplaintEventId { get; set; }
        /// <summary>
        /// Doagadjaj na osnovu zalbe
        /// </summary>
        public string Event { get; set; }
    }
}
