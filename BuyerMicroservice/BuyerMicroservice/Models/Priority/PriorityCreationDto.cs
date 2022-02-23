using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyerMicroservice.Models.Priority
{
<<<<<<< HEAD
    /// <summary>
    /// Priority creation DTO model for communication with user
    /// </summary>
    public class PriorityCreationDto
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
=======
    public class PriorityCreationDto
    {
        public Guid priorityID { get; set; }

>>>>>>> fc78dec60f56cc4dd7d5724adaa6d44b10ccb90a
        public string priorityType { get; set; }
    }
}
