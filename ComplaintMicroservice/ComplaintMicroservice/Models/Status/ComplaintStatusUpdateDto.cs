using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintMicroservice.Models.ComplaintStatusDto
{
    /// <summary>
    /// Status zalbe DTO za update
    /// </summary>
    public class ComplaintStatusUpdateDto
    {
        /// <summary>
        /// Status zalbe ID
        /// </summary>
        public Guid ComplaintStatusId { get; set; }

        /// <summary>
        /// Status zalbe 
        /// </summary>
        [Required(ErrorMessage = "Must enter a type.")]
        public string Status { get; set; }
    }
}
