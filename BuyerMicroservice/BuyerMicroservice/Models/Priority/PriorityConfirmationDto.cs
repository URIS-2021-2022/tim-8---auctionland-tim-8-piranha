using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyerMicroservice.Models.Priority
{
    /// <summary>
    /// Priority confirmation  DTO model for communication with user
    /// </summary>
    public class PriorityConfirmationDto
    {
        /// <summary>
        ///  Priority Type - tip prioriteta 
        ///  Example : 1
        /// </summary>
        public string priorityType { get; set; }
    }
}
