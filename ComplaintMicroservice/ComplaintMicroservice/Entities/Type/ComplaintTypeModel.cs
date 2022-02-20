using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintMicroservice.Entities
{
    /// <summary>
    /// Predstavlja tip zalbe
    /// </summary>
    public class ComplaintTypeModel
    {
        /// <summary>
        /// Predstavlja ID tipa zalbe
        /// </summary>
        [Key]
        public Guid ComplaintTypeId { get; set; }
        /// <summary>
        /// Tip zalbe
        /// </summary>
        public string ComplaintType { get; set; }
    }
}
