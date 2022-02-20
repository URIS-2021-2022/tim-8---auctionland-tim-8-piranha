using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintMicroservice.Models.ComplaintStatusDto
{
    /// <summary>
    /// Potvrda statusa zalbe DTO
    /// </summary>
    public class ComplaintStatusConfirmationDto
    {
        /// <summary>
        /// Status zalbe ID
        /// </summary>
        public Guid ComplaintStatusId { get; set; }

        /// <summary>
        /// Status zalbe 
        /// </summary>
        public string Status { get; set; }
    }
}
