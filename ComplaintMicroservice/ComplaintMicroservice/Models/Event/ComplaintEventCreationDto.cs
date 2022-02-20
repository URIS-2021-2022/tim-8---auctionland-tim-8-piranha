using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintMicroservice.Models.Event
{

    /// <summary>
    /// Dogadjaj na osnovu zalbe DTO za kreiranje
    /// </summary>
    public class ComplaintEventCreationDto
    {

        /// <summary>
        /// Dogadjaj na osnovu zalbe 
        /// </summary>
        [Required(ErrorMessage = "Must enter an event.")]
        public string Event { get; set; }
    }
}
