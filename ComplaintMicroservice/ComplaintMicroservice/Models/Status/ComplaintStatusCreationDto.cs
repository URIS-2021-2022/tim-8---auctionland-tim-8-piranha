using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintMicroservice.Models.ComplaintStatusDto
{
    /// <summary>
    /// Status zalbe DTO za kreiranje
    /// </summary>
    public class ComplaintStatusCreationDto
    {
        /// <summary>
        /// Status zalbe 
        /// </summary>
        [Required(ErrorMessage = "Must enter a status.")]
        public string Status { get; set; }
    }
}
