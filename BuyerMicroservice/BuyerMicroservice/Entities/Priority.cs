using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BuyerMicroservice.Entities
{
    /// <summary>
    /// Priority model
    /// </summary>
    public class Priority
    {
        /// <summary>
        /// priority ID 
        /// Example:  861f142c-4707-416d-ad14-7debbd2031ed
        /// </summary>
        [Key]
        public Guid priorityID { get; set; }

        /// <summary>
        ///  Priority Type - tip prioriteta 
        ///  Example : 1
        /// </summary>
        public string priorityType { get; set; }
    }
}
