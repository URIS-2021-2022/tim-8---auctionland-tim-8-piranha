using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintMicroservice.Entities
{
    /// <summary>
    /// Predstavlja potvrdu tipa zalbe
    /// </summary>
    public class ComplaintTypeConfirmation
    {
        
        /// /// <summary>
        /// Predstavlja ID tipa zalbe
        /// </summary>
        public Guid ComplaintTypeId { get; set; }
        /// <summary>
        /// Tip zalbe
        /// </summary>
        public string ComplaintType { get; set; }
    }
}
