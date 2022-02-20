using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintMicroservice.Entities.ComplaintStatusEntities
{
    /// <summary>
    /// Predstavlja status zalbe
    /// </summary>
    public class ComplaintStatus
    {
        /// <summary>
        /// ID statusa zalbe
        /// </summary>
        [Key]
        public Guid ComplaintStatusId { get; set; }
        /// <summary>
        /// Status zalbe
        /// </summary>
        public string Status { get; set; }
    }
}
