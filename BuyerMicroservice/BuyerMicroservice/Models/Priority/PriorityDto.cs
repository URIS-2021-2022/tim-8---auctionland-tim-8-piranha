using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyerMicroservice.Models.Priority
{
    /// <summary>
    /// Priority  DTO model for communication with user
    /// </summary>
    public class PriorityDto
    {

        /// <summary>
        ///  Priority Type - tip prioriteta 
        ///  Example : 1
        /// </summary>
        public string priorityType { get; set; }
        
    }
}
