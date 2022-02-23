using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyerMicroservice.Models.Priority
{
    /// <summary>
    /// Priority update DTO model for communication with user
    /// </summary>
    public class PriorityUpdateDto
    {
        /// <summary>
        /// priority ID 
        /// Example:  861f142c-4707-416d-ad14-7debbd2031ed
        /// </summary>
        public Guid priorityID { get; set; }

        /// <summary>
        ///  Priority Type - tip prioriteta 
        ///  Example : 1
        /// </summary>
        public string priorityType { get; set; }
    }
}
