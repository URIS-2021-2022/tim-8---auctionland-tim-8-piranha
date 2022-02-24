using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintMicroservice.Models.Event
{

    /// <summary>
    /// Dogadjaj na osnovu zalbe DTO za update
    /// </summary>
    public class ComplaintEventUpdateDto
    {

        /// <summary>
        /// ID dogadjaja na osnovu zalbe
        /// </summary>
        public Guid ComplaintEventId { get; set; }


        /// <summary>
        /// Dogadjaj na osnovu zalbe 
        /// </summary>
        [Required(ErrorMessage = "Must enter an event.")]
        public string Event { get; set; }
    }
}
