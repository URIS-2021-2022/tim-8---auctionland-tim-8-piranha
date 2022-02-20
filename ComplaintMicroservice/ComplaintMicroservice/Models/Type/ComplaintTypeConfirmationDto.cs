using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintMicroservice.Models
{
    /// <summary>
    /// Potvrda tipa zalbe DTO
    /// </summary>
    public class ComplaintTypeConfirmationDto
    {
        /// <summary>
        /// Tip zalbe ID
        /// </summary>
        public Guid ComplaintTypeId { get; set; }
        /// <summary>
        /// Tip zalbe
        /// </summary>
        public string ComplaintType { get; set; }
    }
}
