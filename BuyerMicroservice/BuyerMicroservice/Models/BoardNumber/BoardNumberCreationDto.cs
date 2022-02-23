using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyerMicroservice.Models.BoardNumber
{
    /// <summary>
    /// Board number creation DTO for communication with user
    /// </summary>
    public class BoardNumberCreationDto
    {
        /// <summary>
        /// board Number ID - ID broja table
        /// Example : 861f142c-4707-416d-ad14-7debbd2031ed
        /// </summary>
        public Guid boardNumberID { get; set; }
        /// <summary>
        /// Number - Broj table
        /// Example : 2
        /// </summary>
        public int number { get; set; }
    }
}
