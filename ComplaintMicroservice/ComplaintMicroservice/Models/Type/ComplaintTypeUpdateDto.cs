using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintMicroservice.Models
{
    /// <summary>
    /// Tip zalbe DTO za update
    /// </summary>
    public class ComplaintTypeUpdateDto
    {
        /// <summary>
        /// ID tipa zalbe
        /// </summary>
        public Guid ComplaintTypeId { get; set; }

        /// <summary>
        /// Tip zalbe
        /// </summary>
        [Required(ErrorMessage = "Must enter a type.")]
        public string ComplaintType { get; set; }
    }
}
